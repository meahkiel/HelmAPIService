using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetLogSheetDraftsQuery(string LvStation) : IRequest<Result<IEnumerable<LogSheetResponse>>>;

public class GetLogSheetDraftQueryHandler : IRequestHandler<GetLogSheetDraftsQuery, Result<IEnumerable<LogSheetResponse>>>
{
    private readonly IUnitWork _unitWork;

    public GetLogSheetDraftQueryHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }
    public async Task<Result<IEnumerable<LogSheetResponse>>> Handle(GetLogSheetDraftsQuery request, CancellationToken cancellationToken)
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
