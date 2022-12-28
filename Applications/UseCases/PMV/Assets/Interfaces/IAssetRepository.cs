using Applications.Interfaces;
using Applications.UseCases.PMV.Assets.DTO;
using Core.PMV.Assets;

namespace Applications.UseCases.PMV.Assets.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{

    Task<IEnumerable<AssetViewResponse>> GetAssets();
    Task<IEnumerable<AssetViewResponse>> GetAssetsByAttribute(string category);
    Task<IEnumerable<AssetViewResponse>> GetAssetsByCode(string assetCode);

    Task<IEnumerable<AssetViewResponse>> ViewAssetById(int assetId);
}