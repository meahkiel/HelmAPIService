using Applications.UseCases.PMV.Fuels.DTO;
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
        private readonly IUserAccessor _userAccessor;
        public UpdateLogCommandHandler(IUnitWork unitWork,IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _unitWork = unitWork;

        }
        public async Task<Result<Unit>> Handle(UpdateLogCommand request, CancellationToken cancellationToken)
        {
            try {

                var log = await _unitWork.FuelLogs.GetLog(request.Request.Id!);
                if (log == null) throw new Exception("Fuel Log not found");

                if(log.Post.IsPosted) 
                    throw new Exception("Fuel Log already posted");


                var employeeCode = request.Request.EmployeeCode ??  await _userAccessor.GetUserEmployeeCode();
                
                if (request.Request.Details != null && request.Request.Details.Count > 0) {
                    foreach (var requestDetail in request.Request.Details) {
                        var fuelDateTime = requestDetail.FuelDate.MergeAndConvert(
                                        requestDetail.FuelTime.ConvertToDateTime()!.Value.ToLongTimeString());
                        
                        if(requestDetail.LogType == EnumLogType.Dispense.ToString()) {
                            log.UpsertDispenseTransaction(requestDetail.AssetCode,
                                0,requestDetail.Reading,
                                requestDetail.OperatorDriver ?? "",
                                fuelDateTime,
                                requestDetail.Quantity,
                                requestDetail.DriverQatarIdUrl ?? "",
                                request.Request.Id);
                        }
                        else {
                            
                            log.UpsertRestockTransaction(
                                requestDetail.AssetCode,
                                fuelDateTime,
                                requestDetail.Quantity,
                                requestDetail.Id!);
                        }
                    }
                }
                

                //request to post
                if(!request.IsPartial && request.Request.IsPosted) {
                    log.ShiftEndTime = request.Request.ShiftEndTime.ConvertToDateTime();
                    log.EndShiftTankerKm = request.Request.EndShiftTankerKm;
                    log.TogglePost();
                }
                
                _unitWork.FuelLogs.UpdateLog(log);
                await _unitWork.CommitSaveAsync(employeeCode);

                return Result.Ok(Unit.Value);
            }
            catch(Exception ex) {
                return Result.Fail(ex.Message);
            }

        }
    }
}