using Applications.Interfaces;
using Applications.UseCases.PMV.Assets.DTO;
using Core.PMV.Alerts;
using Core.PMV.Assets;

namespace Applications.UseCases.PMV.Assets.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    bool AddServiceLog(ServiceLog serviceLog);
    bool AddAssetRecord(AssetRecord record);
    bool UpdateAssetRecord(AssetRecord record);

    Task<IEnumerable<AssetListResponse>> GetAssets();
    Task<IEnumerable<AssetListResponse>> GetAssetsByAttribute(string category);
    Task<IEnumerable<AssetListResponse>> GetAssetsByCode(string assetCode);

    Task<AssetViewResponse?> ViewAssetById(int assetId);
  
}