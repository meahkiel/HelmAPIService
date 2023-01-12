using Applications.Interfaces;
using Applications.UseCases.Common;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.Interfaces.DTO;
using Core.Common;
using Core.PMV.Assets;
using Core.PMV.Fuels;
using Dapper;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.DataService;

public class CommonService : ICommonService
{
    private readonly PMVDataContext _context;

    public CommonService(IDataContext context)
    {
        _context = (PMVDataContext)context;
    }

    public async Task<AutoNumber> GenerateAutoNumber(string type, string source)
    {
          var results = await _context.Database.GetDbConnection().QueryAsync<AutoNumber>(
                "sp_GenerateSequenceNo",
                new { Type = type, Source = source},
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
    }

    public async Task<IEnumerable<Station>> GetAllStation()
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<Station>(
                "sp_GetStationByCode",
                commandType: System.Data.CommandType.StoredProcedure);
        
        return results.ToList();
    }

    public async Task<Asset> GetAssetByCode(string code)
    {
         var results = await _context.Database.GetDbConnection().QueryAsync<Asset>(
                "sp_PMVAsset_GetAssetByCode",
                new { assetCode = code},
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
    }

    public async Task<IEnumerable<SelectItem>> GetAssetLookup()
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<SelectItem>(
              "sp_AssetLookup",
              commandType: System.Data.CommandType.StoredProcedure);

        return results;
    }

    public async Task<IEnumerable<EmployeeMaster>> GetEmployees()
    {
          var results = await _context.Database.GetDbConnection().QueryAsync<EmployeeMaster>(
                "sp_GetPMVEmployees",
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results;
    }

    public async Task<IEnumerable<Asset>> GetInternalAssets()
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<Asset>(
                "sp_PMVAsset_GetAssets",
                commandType: System.Data.CommandType.StoredProcedure);
        
        return results;
    }

    public async Task<LocationResponse?> GetLocationByKey(string code,string type = "code",int id=0)
    {   
        
            var results = await _context.Database.GetDbConnection().QueryAsync<LocationResponse>(
                "sp_GetLocationByCode",
                new { code = code, type = type, id = id },
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
        
    }

    public async Task<IEnumerable<LocationResponse>> GetLocations()
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<LocationResponse>(
                "sp_GetLocationByCode",
                new { code = "", type = "all", id = 0 },
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results;
    }

    public async Task<Station?> GetStationByCode(string code)
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<Station>(
                "sp_GetStationByCode",
                new { code = code},
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
    }
}