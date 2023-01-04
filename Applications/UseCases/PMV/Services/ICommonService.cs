using Applications.UseCases.PMV.LogSheets.Interfaces.DTO;
using Applications.UseCases.PMV.Services.DTO;

namespace Applications.UseCases.PMV.Common;

public interface ICommonService
{
    public Task<LocationResponse?> GetLocationByKey(string code,string type = "code",int id=0);
    
    public Task<StationResponse?> GetStationByCode(string code);
}