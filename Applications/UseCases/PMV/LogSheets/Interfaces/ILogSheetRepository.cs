using Applications.UseCases.PMV.LogSheets.DTO;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Interfaces;

public interface ILogSheetRepository : IRepository<LogSheet>
{

    Task<IEnumerable<FuelLogTransactionsResponse>> GetTransactions(string dateFrom,string dateTo);
    Task<IEnumerable<FuelLogTransactionsResponse>> GetPostedTransactionsByAsset(string assetCode);
    Task<IEnumerable<LogSheetResponse?>> GetDraftSheetsByStation(string station);
    Task<LogSheet?> GetSingleLogSheet(Guid id);
    Task<LogSheet?> GetDraft(Guid id);
    Task<LogSheet?> GetLatestRecord();

    bool UpdateDetail(LogSheetDetail detail);
    bool AddDetail(LogSheetDetail detail);

    bool DeleteDetail(LogSheetDetail detail);
}