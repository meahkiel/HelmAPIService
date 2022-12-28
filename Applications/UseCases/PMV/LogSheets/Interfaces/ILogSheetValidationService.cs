using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Interfaces
{
    public interface ILogSheetService
    {
        Task<LogSheetDetail> GetLatestFuelLogRecord(string assetCode,int currentReading);
    }
}