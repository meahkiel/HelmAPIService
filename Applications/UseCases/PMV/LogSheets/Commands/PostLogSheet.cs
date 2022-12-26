

using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Commands;

public record PostLogSheetRequest(LogSheetCloseRequest LogSheetRequest) : IRequest<Result<Unit>>;


public class PostLogSheetValidator : AbstractValidator<LogSheetCloseRequest> {

}

public class PostLogSheetRequestHandler : IRequestHandler<PostLogSheetRequest, Result<Unit>>
{
    private readonly IUnitWork _unitWork;
    private readonly IValidator<LogSheetCloseRequest> _validator;

    public PostLogSheetRequestHandler(IUnitWork unitWork,IValidator<LogSheetCloseRequest> validator)
    {
        _unitWork = unitWork;
        _validator = validator;
    }
    public async Task<Result<Unit>> Handle(PostLogSheetRequest request, CancellationToken cancellationToken)
    {
        try {

            LogSheet logsheet = await GetOpenLogSheet(request.LogSheetRequest);
            logsheet.EndShiftMeterReading = request.LogSheetRequest.EndShiftMeterReading;
            logsheet.EndShiftTankerKm = request.LogSheetRequest.EndShiftMeterReading;
            logsheet.ShiftEndTime = DateTime.Parse(request.LogSheetRequest.ShiftEndTime!);
            logsheet.Remarks = request.LogSheetRequest.Remarks;
            logsheet.Posted();

            _unitWork.LogSheets.Update(logsheet);

             return Result.Ok(Unit.Value);

        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }

    private async Task<LogSheet> GetOpenLogSheet(LogSheetCloseRequest request)
        {
            //get the selected logsheet
            var logsheet = await _unitWork.LogSheets
                                    .GetSingleLogSheet(Guid.Parse(request.Id!));

            if (logsheet == null)
                throw new Exception("Log sheet not found");
            
            if(logsheet.Post.IsPosted)
                throw new Exception("Log sheet is posted already");

            return logsheet;
        }
}


