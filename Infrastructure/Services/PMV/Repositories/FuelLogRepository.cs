using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;
using Core.Utils;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;

public class FuelLogRepository : IFuelLogRepository
{
    private readonly PMVDataContext _context;

    public FuelLogRepository(PMVDataContext context)
    {
        _context = context;
    }

    public void AddFuelLog(FuelLog log)
    {
        _context.FuelLogs.Add(log);
    }

    public async Task<IEnumerable<FuelLog>> GetDraftLogs(string fueler)
    {
        return await _context.FuelLogs
                    .Include(c => c.FuelTransactions)
                    .Where(c => c.Post.IsPosted == false)
                    .Where(c => c.Fueler == fueler)
                    .ToListAsync();
    }

    public Task<FuelLog?> GetLog(string id)
    {
        return _context.FuelLogs
            .Include(f => f.FuelTransactions)
            .SingleOrDefaultAsync(f => f.Id == Guid.Parse(id));
    }

    public async Task<FuelLog> GetSingleLog(string id)
    {
         return await _context.FuelLogs.FindAsync(Guid.Parse(id));
    }
   

    public async Task<IEnumerable<FuelLog>> GetTankTransactions(string fuelStation, DateTime dateFrom, DateTime dateTo)
    {
        return await _context.FuelLogs
                        .Include(f => f.FuelTransactions)
                        .Where(f => f.StationCode == fuelStation)
                        .Where(f => f.Date >= dateFrom && f.Date <= dateTo)
                        .ToListAsync();
    }

    public void UpdateLog(FuelLog log)
    { 
       _context.FuelLogs.Update(log);
    }

    public async Task RemoveTransactions(string[] ids)
    {
        foreach (var id in ids)
        {
            var transaction = await _context.FuelTransactions.FindAsync(Guid.Parse(id));
            if(transaction != null) 
                _context.FuelTransactions.Remove(transaction);
        }
    }

    public void AddTransactionLog(FuelTransaction transaction)
    {
        _context.FuelTransactions.Add(transaction);
    }

    public void UpdateTransactionLog(FuelTransaction transaction)
    {
        _context.FuelTransactions.Update(transaction);
    }

    public async Task<FuelLog?> GetLastFuelLog(string station)
    {
        //get the latest record 
        return await _context.FuelLogs
                .Include(f => f.FuelTransactions)
                .Where(l => l.StationCode == station)
                .OrderByDescending(l => l.ShiftStartTime)
                .OrderByDescending(l => l.DocumentNo)
                .FirstOrDefaultAsync();
    }
}
