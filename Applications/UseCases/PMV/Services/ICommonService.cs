using Applications.UseCases.PMV.Services.DTO;

namespace Applications.UseCases.PMV.Common;

public interface ICommonService
{
    public Task<LocationResponse?> GetLocationByCode(string code);
}