using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.Fuels.Notifications;
using Core.PMV.Fuels;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record UpdateLogCommand(FuelLogRequest Request,bool IsPartial = false) : IRequest<Result<Unit>>;

public class UpdateLogValidator : AbstractValidator<FuelLogRequest> {
    
    
    public UpdateLogValidator(IUnitWork unitWork)
    {
        
    }
}
public class UpdateLogCommandHandler : IRequestHandler<UpdateLogCommand, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private IFuelLogService _fuelService;
    private readonly IUserAccessor _userAccessor;
    private readonly INotificationPublisher _publisher;
    private readonly ICommonService _commonService;

    public UpdateLogCommandHandler(IUnitWork unitWork,IUserAccessor userAccessor,
        INotificationPublisher publisher,IFuelLogService fuelService,ICommonService commonService)
    {
        _commonService = commonService;
        _userAccessor = userAccessor;
        _publisher = publisher;
        _unitWork = unitWork;
        _fuelService = fuelService;

    }
    public async Task<Result<Unit>> Handle(UpdateLogCommand request, CancellationToken cancellationToken)
    {
        try {
            

            var log = await _unitWork.FuelLogs.GetSingleLog(request.Request.Id!);
            if (log == null) throw new Exception("Fuel Log not found");

            if(log.Post.IsPosted) 
                throw new Exception("Fuel Log already posted");

            var employeeCode = request.Request.EmployeeCode ??  await _userAccessor.GetUserEmployeeCode();
            
            
            log.ShiftStartTime = request.Request.ShiftStartTime.ConvertToDateTime();
            log.ShiftEndTime = request.Request.ShiftEndTime.ConvertToDateTime();
            log.StartShiftTankerKm = request.Request.StartShiftTankerKm;
            log.EndShiftTankerKm = request.Request.EndShiftTankerKm;
            log.Fueler = request.Request.Fueler;


            //check item for delete
            if(request.Request.DelCollection != null) {
               await _unitWork.FuelLogs.RemoveTransactions(request.Request.DelCollection.Ids);
            }

            if (request.Request.FuelTransactions != null && request.Request.FuelTransactions.Count() > 0) {

                var stations = await _commonService.GetAllStation();
                foreach (var detail in request.Request.FuelTransactions) {

                    Guid guid = string.IsNullOrEmpty(detail.Id) ? Guid.NewGuid() : Guid.Parse(detail.Id);
                    var result = await _unitWork.GetContext().FuelTransactions
                                        .SingleOrDefaultAsync(l => l.Id == guid);
                    
                    if(result == null) {

                        var previousReading = 0;

                        if(detail.LogType == EnumLogType.Dispense.ToString()) {
                            string sourceType = "";
                            if(!stations.Any(s => s.Code == detail.AssetCode)) {
                                var prevRecord = await _fuelService.GetLatestFuelLogRecord(detail.AssetCode, detail.Reading);
                                previousReading = prevRecord.Reading;
                                sourceType = "Equipment";
                            }
                            else {
                                sourceType = "Tank";
                            }

                            result = new FuelTransaction(guid,
                                detail.AssetCode,
                                previousReading,
                                detail.Reading,
                                detail.OperatorDriver ?? "",
                                log.StationCode,
                                log.Date!.Value.ToShortDateString(),
                                detail.FuelTime,
                                detail.Quantity,
                                detail.LogType,
                                sourceType);

                            result.Remarks = request.Request.Remarks;
                        }
                        else {
                            result = new FuelTransaction(guid,detail.AssetCode,log.StationCode,log.Date!.Value.ToShortDateString(),detail.FuelTime,detail.Quantity);
                        }

                        result.FuelLog = log;
                        _unitWork.FuelLogs.AddTransactionLog(result);
                    }
                    else {
                        if(detail.LogType == EnumLogType.Dispense.ToString() && result.IsLessThanPrevious(detail.Reading)) {
                            
                            if(!stations.Any(s => s.Code == detail.AssetCode))
                                result.SourceType = "Equipment";
                            else
                                result.SourceType = "Tank";

                            result.Reading = detail.Reading;
                        }

                        result.AssetCode = detail.AssetCode;
                        result.Driver = detail.OperatorDriver;
                        result.Quantity = detail.Quantity;
                        result.LogType = detail.LogType;
                        result.Remarks = detail.Remarks;
                        _unitWork.FuelLogs.UpdateTransactionLog(result);
                    }
                }
            }
            
            //request to post
            if(request.Request.IsPosted) {
                log.TogglePost();
                _publisher.Add(new DispenseCreatedNotification(log.FuelTransactions));
            }
            
            await _unitWork.FuelLogs.UpdateLog(log);
            //publish all notification
            await _publisher.Publish();
            await _unitWork.CommitSaveAsync(employeeCode);

            return Result.Ok(Unit.Value);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }

    }
}