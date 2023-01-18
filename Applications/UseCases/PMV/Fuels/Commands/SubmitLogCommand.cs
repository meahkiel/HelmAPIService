using Applications.UseCases.PMV.Fuels.DTO;
using Core.Utils;

namespace Applications.UseCases.PMV.Fuels.Commands;

public record SubmitLogCommand(FuelLogCloseRequest Request) : IRequest<Result<Unit>>;

public class SubmitLogCommandHandler : IRequestHandler<SubmitLogCommand, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly IUserAccessor _userAccessor;

    public SubmitLogCommandHandler(IUnitWork unitWork,IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
        _unitWork = unitWork;
    }

    public async Task<Result<Unit>> Handle(SubmitLogCommand request, CancellationToken cancellationToken)
    {

        try {
            
            var log = await _unitWork.FuelLogs
                                .GetSingleLog(request.Request.Id);

            var employeeCode = request.Request.EmployeeCode ?? 
                                await _userAccessor.GetUserEmployeeCode();
            
            if (log == null) throw new Exception("Fuel Log not found");

            if (log.Post.IsPosted)
                throw new Exception("Fuel Log already posted");

            log.ShiftEndTime = request.Request.ShiftEndTime.ConvertToDateTime();
            log.EndShiftTankerKm = request.Request.EndShiftTankerKm;
            log.Remarks = request.Request.Remarks;
            log.TogglePost(); //post
            
            _unitWork.FuelLogs.UpdateLog(log);
            
            await _unitWork.CommitSaveAsync(employeeCode);
            
            return Result.Ok(Unit.Value);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }


    }
}

