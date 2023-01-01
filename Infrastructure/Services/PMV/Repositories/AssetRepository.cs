using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.Assets.Interfaces;
using Core.PMV.Alerts;
using Core.PMV.Assets;
using Dapper;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly PMVDataContext _context;

    public AssetRepository(PMVDataContext context)
    {
        _context = context;
    }
    
    #region [not implemented]
    public void Update(Asset value)
    {
        throw new NotImplementedException();
    }
    public void Add(Asset value) => throw new NotImplementedException();
    public Task<Asset?> GetByIdAsync(Guid Id) => throw new NotImplementedException();
    public Task<IEnumerable<Asset>> GetAll() => throw new NotImplementedException();
    #endregion

    public async Task<IEnumerable<AssetListResponse>> GetAssets() => await GetAllAssets();
    public async Task<IEnumerable<AssetListResponse>> GetAssetsByAttribute(string category) => await GetAllAssets(new { category = category });

    public async Task<IEnumerable<AssetListResponse>> GetAssetsByCode(string assetCode) => await GetAllAssets(new { assetCode = assetCode });


    public async Task<AssetViewResponse?> ViewAssetById(int assetId)
    {
        var connection = _context.Database.GetDbConnection();
        var results = await connection.QueryAsync<AssetViewResponse>(
            "sp_PMVAsset_ViewAsset",
            new { AssetId = assetId },
            commandType: System.Data.CommandType.StoredProcedure);

        return results.FirstOrDefault();
    }

    private async Task<IEnumerable<AssetListResponse>> GetAllAssets(object? param = null) {

        var connection = _context.Database.GetDbConnection();
        var results = await connection.QueryAsync<AssetListResponse>(
            "sp_PMVAsset_GetAssets",param,
            commandType: System.Data.CommandType.StoredProcedure);
        
        return results;
    }

   

    
}