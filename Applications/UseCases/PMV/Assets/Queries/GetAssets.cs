using Applications.UseCases.PMV.Assets.DTO;

namespace Applications.UseCases.PMV.Assets.Queries
{
    public record GetAssetsRequest(string? Category,string? AssetCode) : IRequest<Result<IEnumerable<AssetListResponse>>>;

    public class GetAssetsRequestHandler : IRequestHandler<GetAssetsRequest, Result<IEnumerable<AssetListResponse>>>
    {
        private readonly IUnitWork _unitWork;

        public GetAssetsRequestHandler(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        
        public async Task<Result<IEnumerable<AssetListResponse>>> Handle(GetAssetsRequest request, CancellationToken cancellationToken)
        {
            try {
                
                IEnumerable<AssetListResponse>? assets = null;
                
                if(!string.IsNullOrEmpty(request.AssetCode)) {
                    assets  = await _unitWork.Assets.GetAssetsByCode(request.AssetCode);
                }
                else if(!string.IsNullOrEmpty(request.Category)) {
                    assets  = await _unitWork.Assets.GetAssetsByAttribute(request.Category);
                }
                else {
                    assets = await _unitWork.Assets.GetAssets();
                }

                return Result.Ok(assets);
            }
            catch(Exception ex) {
                return Result.Fail(ex.Message);
            }
        }
    }
}