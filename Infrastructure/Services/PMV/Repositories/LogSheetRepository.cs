using Applications.UseCases.PMV.LogSheets.DTO;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Core.PMV.LogSheets;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;

public class LogSheetRepository : ILogSheetRepository
{
    private readonly PMVDataContext _context;

    public LogSheetRepository(PMVDataContext context)
    {
        _context = context;
    }
    public void Add(LogSheet value)
    {
        _context.LogSheets.Add(value);
    }

    public bool AddDetail(LogSheetDetail detail)
    {
        throw new NotImplementedException();
    }

    public bool DeleteDetail(LogSheetDetail detail)
    {
        throw new NotImplementedException();
    }

   
    public Task<IEnumerable<LogSheet>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<LogSheet?> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LogSheetResponse?>> GetByStation(string station)
    {
        throw new NotImplementedException();
    }

    public async Task<LogSheet?> GetDraft(Guid id)
    {
        return await _context.LogSheets.FirstOrDefaultAsync(l => l.Post.IsPosted == false && l.Id == id);
    }

    public async Task<LogSheet?> GetLatestRecord()
    {
        return await _context.LogSheets
                .OrderBy(l => l.CreatedAt)
                .OrderBy(l => l.ReferenceNo)
                .LastOrDefaultAsync();
    }

    public Task<LogSheet?> GetSingleLogSheet(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FuelLogTransactionsResponse>> GetTransactions(string dateFrom, string dateTo)
    {
        throw new NotImplementedException();
    }

    public void Update(LogSheet value)
    {
        _context.LogSheets.Update(value);
    }

    public bool UpdateDetail(LogSheetDetail detail)
    {
        throw new NotImplementedException();
    }
}