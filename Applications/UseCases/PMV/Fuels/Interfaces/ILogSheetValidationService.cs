using Core.PMV.Fuels;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.Fuels.Interfaces
{
    public interface IFuelLogService
    {
        Task<FuelTransaction> GetLatestFuelLogRecord(string assetCode,int currentReading);
    }
}