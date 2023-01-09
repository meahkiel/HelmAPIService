using Applications.UseCases.Common;
using Applications.UseCases.PMV.LogSheets.Interfaces.DTO;
using Applications.UseCases.PMV.Services.DTO;
using Core.Common;
using Core.PMV.Assets;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Common;

public interface ICommonService
{
    public Task<LocationResponse?> GetLocationByKey(string code,string type = "code",int id=0);
    public Task<IEnumerable<LocationResponse>> GetLocations();
    
    public Task<Station?> GetStationByCode(string code);
    public Task<IEnumerable<Station>> GetAllStation();

    public Task<Asset> GetAssetByCode(string code);

    public Task<AutoNumber> GenerateAutoNumber(string type,string source);

    public Task<IEnumerable<SelectItem>> GetAssetLookup();

    public Task<IEnumerable<EmployeeMaster>> GetEmployees();
}