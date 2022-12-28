using Applications.UseCases.PMV.Assets.DTO;
using Applications.UseCases.PMV.Assets.Interfaces;
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
    public void Add(Asset value)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Asset>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AssetViewResponse>> GetAssets() => await GetAllAssets();

    public async Task<IEnumerable<AssetViewResponse>> GetAssetsByAttribute(string category) => await GetAllAssets(new { category = category });

    public async Task<IEnumerable<AssetViewResponse>> GetAssetsByCode(string assetCode) => await GetAllAssets(new { assetCode = assetCode });

    public Task<Asset?> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public void Update(Asset value)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AssetViewResponse>> ViewAssetById(int assetId)
    {
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<AssetViewResponse>> GetAllAssets(object? param = null) {

        var connection = _context.Database.GetDbConnection();
        var results = await connection.QueryAsync<AssetViewResponse>(
            "sp_PMVAsset_GetAssets",param,
            commandType: System.Data.CommandType.StoredProcedure);
        
        return results;
    }
}