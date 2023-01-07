using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Interfaces
{
    public interface IFuelLogService
    {
        Task<LogSheetDetail> GetLatestFuelLogRecord(string assetCode,int currentReading);
    }
}