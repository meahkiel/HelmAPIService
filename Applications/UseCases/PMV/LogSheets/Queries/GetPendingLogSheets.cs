using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetPendingLogSheetsRequest(string LvStation) : IRequest<Result<IEnumerable<LogSheetResponse>>>;

public class GetPendingLogSheetsRequestHandler : IRequestHandler<GetPendingLogSheetsRequest, Result<IEnumerable<LogSheetResponse>>>
{
    private readonly IUnitWork _unitWork;

    public GetPendingLogSheetsRequestHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }
    public async Task<Result<IEnumerable<LogSheetResponse>>> Handle(GetPendingLogSheetsRequest request, CancellationToken cancellationToken)
    {
        try {
            
            var response = await _unitWork.LogSheets
                                    .GetDraftSheetsByStation(request.LvStation) ?? new List<LogSheetResponse>();
            
            return Result.Ok(response);
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
