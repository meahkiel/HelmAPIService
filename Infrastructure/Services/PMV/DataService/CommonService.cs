using Applications.Interfaces;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.LogSheets.Interfaces.DTO;
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
    public async Task<LocationResponse?> GetLocationByKey(string code,string type = "code",int id=0)
    {   
        
            var results = await _context.Database.GetDbConnection().QueryAsync<LocationResponse>(
                "sp_GetLocationByCode",
                new { code = code, type = type, id = id },
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
        
    }

    public Task<LocationResponse?> GetLocationById(string code)
    {
        throw new NotImplementedException();
    }
}