using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;

using Core.PMV.Fuels;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record CreateLogCommand(FuelLogRequest Request) : IRequest<Result<FuelLogKeyResponse>>;

public class CreateLogValidator : AbstractValidator<FuelLogRequest> {
    
    public CreateLogValidator()
    {
        
    }
}

public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand, Result<FuelLogKeyResponse>>
{
    private readonly IUnitWork _unitWork;
    private readonly ICommonService _commonService;
    private readonly IFuelLogService _fuelService;
    private readonly INotificationPublisher _publisher;
    private readonly IUserAccessor _userAccessor;

    public CreateLogCommandHandler(
        IUnitWork unitWork,
        ICommonService commonService,
        IFuelLogService fuelService,
        INotificationPublisher publisher, 
        IUserAccessor userAccessor)
    {
        _fuelService = fuelService;
        _publisher = publisher;
        _userAccessor = userAccessor;
        _commonService = commonService;
        _unitWork = unitWork;
    }


    public async Task<Result<FuelLogKeyResponse>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
    {
        try
        {   
            FuelLog log = await GenerateInitialLog(request.Request);
            var employeeCode = string.IsNullOrEmpty(request.Request.EmployeeCode) ? await _userAccessor.GetUserEmployeeCode() : request.Request.EmployeeCode;

            if (request.Request.FuelTransactions.Count() > 0) {
                 var stations = await _commonService.GetAllStation();
                foreach (var detail in request.Request.FuelTransactions)
                {
                    if (detail.LogType != EnumLogType.Dispense.ToString())
                    {
                       
                        int previousReading = 0;
                        string sourceType = "";
                        if(!stations.Any(s => s.Code == detail.AssetCode)) {
                            var prevRecord = await _fuelService.GetLatestFuelLogRecord(detail.AssetCode, detail.Reading);
                            previousReading = prevRecord.Reading;
                            sourceType = "Equipment";
                        }
                        else {
                            sourceType = "Tank";
                        }

                        log.UpsertDispenseTransaction(
                            detail.AssetCode,
                            previousReading,
                            detail.Reading,
                            detail.OperatorDriver ?? "",
                            request.Request.FueledDate,
                            detail.FuelTime,
                            detail.Quantity,
                            request.Request.Remarks,
                            detail.DriverQatarIdUrl ?? "",
                            detail.Id,
                            sourceType);
                    }
                    else
                    {
                        log.UpsertRestockTransaction(detail.AssetCode, request.Request.FueledDate,detail.FuelTime, detail.Quantity);
                    }
                }
            }

            //request to post
            if (request.Request.IsPosted) {
                log.TogglePost();
            }

            _unitWork.FuelLogs.AddFuelLog(log);

            //publish all notification
            //await _publisher.Publish();

            await _unitWork.CommitSaveAsync(employeeCode);

            return Result.Ok(new FuelLogKeyResponse { 
                Id = log.Id.ToString(), 
                DocumentNo = log.DocumentNo, 
                OpeningMeter = log.OpeningMeter });
        }
        catch (Exception ex) {
            return Result.Fail(ex.Message);
        }
    }

    private async Task<FuelLog> GenerateInitialLog(FuelLogRequest request)
    {

        var location = await _commonService.GetLocationByKey(request.Location);
        if (location == null) throw new Exception("location cannot be found");

        //get the latest record 
        
        FuelLog? record = await _unitWork.GetContext().FuelLogs
                            .Include(f => f.FuelTransactions)
                            .Where(l => l.StationCode == request.Station)
                            .OrderByDescending(l => l.ShiftStartTime)
                            .FirstOrDefaultAsync();

        float openingMeter = 0f;
        float openingBalance = 0f;
        var autoNumber = await _commonService.GenerateAutoNumber("DocumentNo","FuelLog");

        if (record != null) {
            openingMeter = record.ClosingMeter;
            openingBalance = record.RemainingBalance;
        }
        else {
            
            var station = await _commonService.GetStationByCode(request.Station);

            if(station != null) {
                openingMeter = station.OpeningMeter;
                openingBalance = station.OpeningBalance;
            }
        }

        DateTime? shiftStartTime = DateTime.Now;
        DateTime? endShifTime = DateTime.Now;

        if(!request.IsPartial) { 
            shiftStartTime = request.ShiftStartTime.ConvertToDateTime();
            endShifTime = request.ShiftEndTime!.ConvertToDateTime();
        }


        var log = FuelLog.Create(
            request.Station,
            location.Id,
            autoNumber.CurrentNumber,
            request.ReferenceNo,
            shiftStartTime,
            endShifTime,
            request.StartShiftTankerKm,
            request.EndShiftTankerKm,
            openingMeter,
            openingBalance, 0, 0);
        
        log.Fueler = request.Fueler;

        return log;
    }
}
