using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.Fuels.Notifications;
using Core.PMV.Fuels;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record UpdateLogCommand(FuelLogRequest Request, bool IsPartial = false) : IRequest<Result<Unit>>;

public class UpdateLogValidator : AbstractValidator<FuelLogRequest>
{


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

    public UpdateLogCommandHandler(IUnitWork unitWork, IUserAccessor userAccessor,
        INotificationPublisher publisher, IFuelLogService fuelService, ICommonService commonService)
    {
        _commonService = commonService;
        _userAccessor = userAccessor;
        _publisher = publisher;
        _unitWork = unitWork;
        _fuelService = fuelService;

    }

    private async Task<FuelLog> GetHeader(FuelLogRequest request)
    {

        var log = await _unitWork.FuelLogs.GetSingleLog(request.Id!);

        if (log == null) throw new Exception("Fuel Log not found");

        if (log.Post.IsPosted)
            throw new Exception("Fuel Log already posted");

        if (!request.IsPartial) {
            log.ShiftStartTime = request.ShiftStartTime.ConvertToDateTime();
            log.ShiftEndTime = request.ShiftEndTime.ConvertToDateTime();
            log.StartShiftTankerKm = request.StartShiftTankerKm;
            log.EndShiftTankerKm = request.EndShiftTankerKm;
            log.Fueler = request.Fueler;

            //check item for delete
            if (request.DelCollection != null)
            {
                await _unitWork.FuelLogs.RemoveTransactions(request.DelCollection.Ids);
            }

            if(request.IsPosted) {
                log.ShiftEndTime = request.ShiftEndTime.ConvertToDateTime();
                log.EndShiftTankerKm = request.EndShiftTankerKm;
                log.TogglePost();
                _publisher.Add(new DispenseCreatedNotification(log.FuelTransactions));
            }
        }
        else {
            if(request.IsPosted) {
                log.ShiftEndTime = request.ShiftEndTime.ConvertToDateTime();
                log.EndShiftTankerKm = request.EndShiftTankerKm;
                log.TogglePost();
            }
        }

        return log;
    }

    

    public async Task<Result<Unit>> Handle(UpdateLogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var log = await GetHeader(request.Request);
            var employeeCode = request.Request.EmployeeCode ?? await _userAccessor.GetUserEmployeeCode();
            if (request.Request.FuelTransactions != null && request.Request.FuelTransactions.Count() > 0)
            {
                var stations = await _commonService.GetAllStation();
                foreach (var detail in request.Request.FuelTransactions)
                {
                    Guid guid = string.IsNullOrEmpty(detail.Id) ? Guid.NewGuid() : Guid.Parse(detail.Id);
                    var transaction = await _unitWork.GetContext().FuelTransactions
                                        .SingleOrDefaultAsync(l => l.Id == guid);

                    if (transaction == null)
                    {
                        var previousReading = 0;
                        if (detail.LogType == EnumLogType.Dispense.ToString())
                        {
                            string sourceType = "";
                            if (!stations.Any(s => s.Code == detail.AssetCode))
                            {
                                var prevRecord = await _fuelService.GetLatestFuelLogRecord(detail.AssetCode, detail.Reading);
                                previousReading = prevRecord.Reading;
                                sourceType = "Equipment";
                            }
                            else
                            {
                                sourceType = "Tank";
                            }

                            transaction = new FuelTransaction(guid,
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

                            transaction.Remarks = request.Request.Remarks;
                        }
                        else
                        {
                            transaction = new FuelTransaction(guid, detail.AssetCode, log.StationCode, log.Date!.Value.ToShortDateString(), detail.FuelTime, detail.Quantity);
                        }

                        transaction.FuelLog = log;
                        _unitWork.FuelLogs.AddTransactionLog(transaction);
                    }
                    else
                    {

                        if (detail.LogType == EnumLogType.Dispense.ToString() && transaction.IsLessThanPrevious(detail.Reading))
                        {
                            if (!stations.Any(s => s.Code == detail.AssetCode))
                                transaction.SourceType = "Equipment";
                            else
                                transaction.SourceType = "Tank";

                            transaction.Reading = detail.Reading;
                        }

                        transaction.AssetCode = detail.AssetCode;
                        transaction.Driver = detail.OperatorDriver;
                        transaction.Quantity = detail.Quantity;
                        transaction.LogType = detail.LogType;
                        transaction.Remarks = detail.Remarks;

                        _unitWork.FuelLogs.UpdateTransactionLog(transaction);
                    }
                }
            }

            _unitWork.FuelLogs.UpdateLog(log);

            //publish all notification
            //await _publisher.Publish();
            
            await _unitWork.CommitSaveAsync(employeeCode);

            return Result.Ok(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }
}