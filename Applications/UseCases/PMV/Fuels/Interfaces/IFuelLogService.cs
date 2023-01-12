using Applications.UseCases.PMV.Fuels.DTO;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Interfaces
{
    public interface IFuelLogService
    {
        Task<FuelTransaction> GetLatestFuelLogRecord(string assetCode,int currentReading);
        Task<IEnumerable<FuelLogMaster>> GetFuelLogMasterList(DateTime dateFrom,DateTime dateTo,string? stationCode = null);
        Task<IEnumerable<FuelLogEffeciencyList>> GetFuelEffeciencyMasterList(DateTime dateFrom,DateTime dateTo,string? assetCodes =null);
    }
}