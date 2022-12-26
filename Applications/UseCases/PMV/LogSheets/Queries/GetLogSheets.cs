using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetLogSheetsRequest(string dateFrom,string dateTo) : IRequest<Result<IEnumerable<FuelLogTransactionsResponse>>>;

public class GetLogSheetsRequestHandler : IRequestHandler<GetLogSheetsRequest, Result<IEnumerable<FuelLogTransactionsResponse>>>
{
    private readonly IUnitWork _unitWork;

    public GetLogSheetsRequestHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }


    public async Task<Result<IEnumerable<FuelLogTransactionsResponse>>> Handle(GetLogSheetsRequest request, CancellationToken cancellationToken)
    {
        try {

            var results = await _unitWork.LogSheets.GetTransactions(request.dateFrom,request.dateTo);
            return Result.Ok(results);
            
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
