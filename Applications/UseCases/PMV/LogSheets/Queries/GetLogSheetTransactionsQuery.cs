using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.LogSheets.Queries;

public record GetLogSheetTransactionsQuery(string dateFrom,string dateTo) : IRequest<Result<FuelLogTransactionsResponse>>;

public class GetLogSheetTransactionsQueryHandler : IRequestHandler<GetLogSheetTransactionsQuery, Result<FuelLogTransactionsResponse>>
{
    private readonly IUnitWork _unitWork;

    public GetLogSheetTransactionsQueryHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }


    public async Task<Result<FuelLogTransactionsResponse>> Handle(GetLogSheetTransactionsQuery request, CancellationToken cancellationToken)
    {
        try {

            var results = await _unitWork.LogSheets.GetTransactions(request.dateFrom,request.dateTo);
            
            return Result.Ok(new FuelLogTransactionsResponse(results));
            
        }
        catch(Exception ex) {
            return Result.Fail(ex.Message);
        }
    }
}
