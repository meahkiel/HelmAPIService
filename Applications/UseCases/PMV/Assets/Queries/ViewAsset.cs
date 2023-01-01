using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.LogSheets.DTO;

namespace Applications.UseCases.PMV.Assets.Queries;

public record ViewAssetRequest(int Id) : IRequest<Result<AssetViewResponse>>;

public class ViewAssetRequestHandler : 
    IRequestHandler<ViewAssetRequest, 
        Result<AssetViewResponse>>
{
    
    private readonly IUnitWork _unitWork;

    public ViewAssetRequestHandler(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }

    public async Task<Result<AssetViewResponse>> Handle(ViewAssetRequest request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var viewAsset = await _unitWork.Assets.ViewAssetById(request.Id);

            if (viewAsset == null)
                throw new Exception("Cannot find asset");

            //get fuel history
            viewAsset.LogSheets = await _unitWork.LogSheets.GetPostedTransactionsByAsset(viewAsset.AssetCode) 
                                            ?? new List<FuelLogTransactionsResponse>();
            
            return Result.Ok(viewAsset);
        }
        catch(Exception ex) {

            return Result.Fail(ex.Message);
        }
    }
}

