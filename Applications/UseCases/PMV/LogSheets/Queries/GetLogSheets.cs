using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetDispenseLogSheetsByDateQuery(string dateFrom,string dateTo) : IRequest<Result<IEnumerable<FuelLogTransactionsResponse>>>;

public class GetDispenseLogSheetsByDateQueryHandler : IRequestHandler<GetDispenseLogSheetsByDateQuery, Result<IEnumerable<FuelLogTransactionsResponse>>>
{
    private readonly IUnitWork _unitWork;

    public GetDispenseLogSheetsByDateQueryHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }


    public async Task<Result<IEnumerable<FuelLogTransactionsResponse>>> Handle(GetDispenseLogSheetsByDateQuery request, CancellationToken cancellationToken)
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
