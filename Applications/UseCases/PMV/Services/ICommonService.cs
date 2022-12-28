using Applications.UseCases.PMV.LogSheets.Interfaces.DTO;

namespace Applications.UseCases.PMV.Common;

public interface ICommonService
{
    public Task<LocationResponse?> GetLocationByKey(string code,string type = "code",int id=0);
    
}