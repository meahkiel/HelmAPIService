using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.Fuels.Notifications;

using Core.PMV.Fuels;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record CreateLogCommand(FuelLogRequest Request) : IRequest<Result<FuelLogKeyResponse>>;

public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand, Result<FuelLogKeyResponse>>
{
    private readonly IUnitWork _unitWork;
    private readonly ICommonService _commonService;
        private readonly IFuelLogService _fuelService;
    private readonly IPublisher _publisher;
    private readonly IUserAccessor _userAccessor;

    public CreateLogCommandHandler(IUnitWork unitWork,
        ICommonService commonService,
        IFuelLogService fuelService,
        IPublisher publisher, 
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
            var employeeCode = request.Request.EmployeeCode ??  await _userAccessor.GetUserEmployeeCode();
            if(request.Request.Details.Count > 0) {
                
                foreach(var detail in request.Request.Details) {
                    var fuelDateTime = detail.FuelDate.MergeAndConvert(
                                    detail.FuelTime.ConvertToDateTime()!.Value.ToLongTimeString());

                    if(detail.LogType == EnumLogType.Dispense.ToString()) 
                    {
                        var prevRecord = await _fuelService.GetLatestFuelLogRecord(detail.AssetCode,detail.Reading);
                        var previousReading = prevRecord.Reading;
                        
                        log.UpsertDispenseTransaction(
                            detail.AssetCode,
                            previousReading,
                            detail.Reading,
                            detail.OperatorDriver ?? "",
                            fuelDateTime,
                            detail.Quantity,
                            detail.DriverQatarIdUrl ?? "", 
                            detail.Id);
                    }
                    else {
                        log.UpsertRestockTransaction(detail.AssetCode,fuelDateTime,detail.Quantity);
                    }
                }
            }

             //request to post
            if(request.Request.IsPosted)
                log.TogglePost();

            _unitWork.FuelLogs.AddFuelLog(log);
            
            //publish log
            await _publisher.Publish(new DispenseCreatedNotification(log.FuelTransactions));
            await _unitWork.CommitSaveAsync(employeeCode);

            return Result.Ok(new FuelLogKeyResponse { Id = log.Id.ToString(), DocumentNo = log.DocumentNo });
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
                            .OrderByDescending(l => l.DocumentNo)
                            .FirstOrDefaultAsync();

        float openingMeter = 0f;
        float openingBalance = 0f;
        var autoNumber = await _commonService.GenerateAutoNumber("DocumentNo","FuelLog");

        if (record != null)
        {
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

        var log = FuelLog.Create(
            request.Station,
            location.Id,
            autoNumber.CurrentNumber,
            request.ReferenceNo,
            request.ShiftStartTime.ConvertToDateTime(),
            request.ShiftEndTime.ConvertToDateTime(),
            request.StartShiftTankerKm,
            request.EndShiftTankerKm,
            openingMeter,
            openingBalance, 0, 0);
        
        return log;
    }
}
