using Applications.Interfaces;
using Core.PMV.LogSheets;

namespace Applications.UseCases.PMV.LogSheets.Interfaces;

public interface ILogSheetRepository : IRepository<LogSheet>
{
    Task<LogSheet?> GetSingleLogSheet(Guid id);
    Task<LogSheet?> GetLatestRecord();

    bool UpdateDetail(LogSheetDetail detail);
    bool AddDetail(LogSheetDetail detail);
    
}