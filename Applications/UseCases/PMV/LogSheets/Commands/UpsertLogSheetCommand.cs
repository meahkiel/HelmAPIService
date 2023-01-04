using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Commands;

public record UpsertLogSheetCommand(LogSheetRequest SheetRequest) : 
    IRequest<Result<Unit>>;


public class UpsertLogSheetValidator : AbstractValidator<LogSheetRequest>
{
    
}

public class UpsertLogSheetCommandHandler : IRequestHandler<UpsertLogSheetCommand, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly ICommonService _commonService;
    private readonly ILogSheetService _validationService;
    private readonly IUserAccessor _userAccessor;
    private readonly IValidator<LogSheetRequest> _validator;

    public UpsertLogSheetCommandHandler(
        IUnitWork unitWork, 
        IValidator<LogSheetRequest> validator,
        ICommonService commonService,
        ILogSheetService validationService,
        IUserAccessor userAccessor) {
        _validator = validator;
        _unitWork = unitWork;
        _commonService = commonService;
        _validationService = validationService;
        _userAccessor = userAccessor;
    }


    public async Task<Result<Unit>> Handle(UpsertLogSheetCommand request, CancellationToken cancellationToken)
    {
        try {

            LogSheet? logsheet = null;
            var employeeCode = await _userAccessor.GetUserEmployeeCode();
            var location = await _commonService.GetLocationByKey(request.SheetRequest.Location);
            if (String.IsNullOrEmpty(request.SheetRequest.Id))
            {
                //create 

                //purpose to obtain the latest ref not much of rep. consider rev.
                var existingLog = await _unitWork.LogSheets.GetLatestRecord() ?? new LogSheet();
                
                //create new logsheet also create unique identifier whithin the creation
                logsheet = LogSheet.Create(existingLog.ReferenceNo,
                                    request.SheetRequest.ShiftStartTime,
                                    request.SheetRequest.StartShiftTankerKm,
                                    request.SheetRequest.StartShiftMeterReading,
                                    location!.Id,
                                    request.SheetRequest.LVStation, request.SheetRequest.Fueler);
                
                //close logsheet post the transaction
                logsheet.CloseLogSheet(
                    request.SheetRequest.EndShiftMeterReading,
                    request.SheetRequest.EndShiftTankerKm,
                    request.SheetRequest.ShiftEndTime,
                    request.SheetRequest.Remarks);
                    
                if (request.SheetRequest.details != null && request.SheetRequest.details.Count > 0) {
                    //upset tank check if the station is main
                    var station = await _commonService.GetStationByCode(logsheet.StationCode);
                    var reading = 0;
                    foreach (var detail in request.SheetRequest.details) {
                        if(station!.StationType != "main") {
                            var prevRecord = await _validationService.GetLatestFuelLogRecord(detail.AssetCode,detail.Reading);
                            reading = prevRecord.Reading;
                        }
                        
                        var logSheetDetail = LogSheetDetail.Create(
                                assetCode: detail.AssetCode,
                                fuelTime: detail.FuelTime,
                                reading: detail.Reading,
                                previousReading: reading,
                                quantity: detail.Quantity,
                                driverQatarIdUrl: detail.DriverQatarIdUrl ?? "",
                                operatorDriver: detail.OperatorDriver,
                                transactionType: detail.TransactionType
                        );
                        logsheet.AddDetail(logSheetDetail);
                    }
                }
                
                _unitWork.LogSheets.Add(logsheet);
            }
            else {
                logsheet = await _unitWork.LogSheets.GetSingleLogSheet(Guid.Parse(request.SheetRequest.Id));
                if (logsheet == null) throw new Exception("Logsheet not found");
                
                logsheet!.ShiftStartTime = DateTime.Parse(request.SheetRequest.ShiftStartTime);
                logsheet!.ShiftEndTime = DateTime.Parse(request.SheetRequest.ShiftEndTime);
                logsheet!.StartShiftTankerKm = request.SheetRequest.StartShiftTankerKm;
                logsheet!.EndShiftTankerKm = request.SheetRequest.EndShiftTankerKm;
                logsheet!.StartShiftMeterReading = request.SheetRequest.StartShiftMeterReading;
                logsheet!.EndShiftMeterReading = request.SheetRequest.EndShiftMeterReading;
                logsheet!.LocationId = location!.Id;
                logsheet!.StationCode = request.SheetRequest.LVStation;
                
                if (request.SheetRequest.details != null && request.SheetRequest.details.Count > 0) 
                {
                    foreach (var requestDetail in request.SheetRequest.details) {
                        logsheet.UpdateDetail(requestDetail.Id!,
                        requestDetail.AssetCode,
                        requestDetail.Reading,requestDetail.Quantity,
                        requestDetail.OperatorDriver!,
                        requestDetail.TransactionType);
                    }
                }
                _unitWork.LogSheets.Update(logsheet);
            }

            //commit save
            await _unitWork.CommitSaveAsync(employeeCode);
            
            return Result.Ok(Unit.Value);
        } 
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }

   

   
}
