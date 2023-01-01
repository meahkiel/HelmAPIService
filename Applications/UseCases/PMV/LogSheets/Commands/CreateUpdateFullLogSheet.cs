using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Commands;

public record CreateUpdateFullLogSheetRequest(LogSheetRequest SheetRequest) : 
    IRequest<Result<Unit>>;


public class CreateUpdateFullLogSheetValidator : AbstractValidator<LogSheetRequest>
{
    
}

public class CreateUpdateFullLogSheetRequestHandler : IRequestHandler<CreateUpdateFullLogSheetRequest, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly ICommonService _commonService;
    private readonly ILogSheetService _validationService;
    private readonly IUserAccessor _userAccessor;
    private readonly IValidator<LogSheetRequest> _validator;

    public CreateUpdateFullLogSheetRequestHandler(
        IUnitWork unitWork, 
        IValidator<LogSheetRequest> validator,
        ICommonService commonService,
        ILogSheetService validationService,
        IUserAccessor userAccessor)
    {
        _validator = validator;
        _unitWork = unitWork;
        _commonService = commonService;
        _validationService = validationService;
        _userAccessor = userAccessor;
    }
    public async Task<Result<Unit>> Handle(CreateUpdateFullLogSheetRequest request, CancellationToken cancellationToken)
    {
        try {

            LogSheet? logsheet = null;
            var employeeCode = await _userAccessor.GetUserEmployeeCode();
            var location = await _commonService.GetLocationByKey(request.SheetRequest.Location);
            if (String.IsNullOrEmpty(request.SheetRequest.Id))
            {


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

                
                if (request.SheetRequest.details != null && request.SheetRequest.details.Count > 0)
                {
                    foreach (var detail in request.SheetRequest.details)
                    {
                        
                        var prevRecord = await _validationService.GetLatestFuelLogRecord(detail.AssetCode,detail.Reading);
                        
                        var logSheetDetail = LogSheetDetail.Create(
                                assetCode: detail.AssetCode,
                                fuelTime: detail.FuelTime,
                                reading: detail.Reading,
                                previousReading: prevRecord.Reading,
                                quantity: detail.Quantity,
                                driverQatarIdUrl: detail.DriverQatarIdUrl ?? "",
                                currentSMUUrl: detail.CurrentSMUUrl ?? "",
                                tankMeterUrl: detail.TankMeterUrl ?? "",
                                operatorDriver: detail.OperatorDriver
                        );
                        
                        logsheet.AddDetail(logSheetDetail);
                    }
                }
                
                _unitWork.LogSheets.Add(logsheet);

            }
            else
            {

                logsheet = await _unitWork.LogSheets.GetDraft(Guid.Parse(request.SheetRequest.Id));

                if (logsheet == null) throw new Exception("Logsheet not found");

                logsheet!.ShiftStartTime = DateTime.Parse(request.SheetRequest.ShiftStartTime);
                logsheet!.ShiftEndTime = DateTime.Parse(request.SheetRequest.ShiftEndTime);
                logsheet!.StartShiftTankerKm = request.SheetRequest.StartShiftTankerKm;
                logsheet!.EndShiftTankerKm = request.SheetRequest.EndShiftTankerKm;
                logsheet!.StartShiftMeterReading = request.SheetRequest.StartShiftMeterReading;
                logsheet!.EndShiftMeterReading = request.SheetRequest.EndShiftMeterReading;
                logsheet!.LocationId = location!.Id;
                logsheet!.LvStationCode = request.SheetRequest.LVStation;

                logsheet.Posted();

                _unitWork.LogSheets.Update(logsheet);

                if (request.SheetRequest.details != null && request.SheetRequest.details.Count > 0)
                {
                    foreach (var requestDetail in request.SheetRequest.details)
                    {
                        
                        var detail = await _unitWork
                                        .GetContext()
                                        .LogSheetDetails
                                        .Where(d => d.Id == Guid.Parse(requestDetail.Id!))
                                        .FirstOrDefaultAsync();
                        
                        if(detail == null) continue;

                        if(requestDetail.IsMarkDeleted) {
                            _unitWork.LogSheets.DeleteDetail(detail);
                        }
                        else {
                            detail.AssetCode = requestDetail.AssetCode;
                            if(detail.IsLessThanPrevious(requestDetail.Reading)) {
                                detail.Reading = requestDetail.Reading;
                            }
                            detail.Quantity = requestDetail.Quantity;
                            detail.OperatorDriver = requestDetail.OperatorDriver;
                        }
                        
                    }
                }
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
