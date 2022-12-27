using Applications.Interfaces;
using Applications.UseCases.PMV.Common;
using Applications.UseCases.PMV.Services.DTO;
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
    public async Task<LocationResponse?> GetLocationByCode(string code)
    {   
        
            var results = await _context.Database.GetDbConnection().QueryAsync<LocationResponse>(
                "sp_GetLocationByCode",
                new { code = code },
                commandType: System.Data.CommandType.StoredProcedure);
            
            return results.FirstOrDefault();
        
    }
}