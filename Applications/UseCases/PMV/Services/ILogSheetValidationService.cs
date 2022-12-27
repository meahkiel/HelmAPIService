using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.Services
{
    public interface ILogSheetValidationService
    {
        Task<LogSheetDetail> GetLatestFuelLogRecord(string assetCode,int currentReading);
    }
}