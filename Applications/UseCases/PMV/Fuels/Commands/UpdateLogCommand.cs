using Applications.UseCases.Common;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.Fuels.Notifications;
using Core.PMV.Fuels;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands
{
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

        public UpdateLogCommandHandler(IUnitWork unitWork,IUserAccessor userAccessor,
            INotificationPublisher publisher,IFuelLogService fuelService)
        {
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
                    foreach (var requestDetail in request.Request.FuelTransactions) {
                        Guid guid = string.IsNullOrEmpty(requestDetail.Id) ? Guid.NewGuid() : Guid.Parse(requestDetail.Id);
                        var result = await _unitWork.GetContext().FuelTransactions
                                            .SingleOrDefaultAsync(l => l.Id == guid);
                        
                        if(result == null) {
                            var prevRecord = await _fuelService.GetLatestFuelLogRecord(requestDetail.AssetCode, requestDetail.Reading);
                            
                            result = new FuelTransaction(guid,
                                requestDetail.AssetCode,
                                prevRecord.Reading,
                                requestDetail.Reading,
                                requestDetail.OperatorDriver ?? "",
                                log.StationCode,
                                log.Date!.Value.ToShortDateString(),
                                requestDetail.FuelTime,
                                requestDetail.Quantity);

                            _unitWork.FuelLogs.AddTransactionLog(result);
                        }
                        else {
                            if(requestDetail.LogType == EnumLogType.Dispense.ToString() && result.IsLessThanPrevious(requestDetail.Reading)) {
                                result.Reading = requestDetail.Reading;
                            }
                            result.AssetCode = requestDetail.AssetCode;
                            result.Driver = requestDetail.OperatorDriver;
                            result.Quantity = requestDetail.Quantity;
                            result.LogType = requestDetail.LogType;
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
}