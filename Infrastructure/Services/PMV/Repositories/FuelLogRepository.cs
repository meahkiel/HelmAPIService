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

    public Task<IEnumerable<FuelLog>> GetDraftLogs(string station)
    {
        throw new NotImplementedException();
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

    


    public Task<IEnumerable<FuelTransactionReport>> GetTransactions(DateTime dateFrom, DateTime dateTo)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<FuelLog>> GetTankTransactions(string fuelStation, DateTime dateFrom, DateTime dateTo)
    {
        return await _context.FuelLogs
                        .Include(f => f.FuelTransactions)
                        .Where(f => f.StationCode == fuelStation)
                        .Where(f => f.Date >= dateFrom && f.Date <= dateTo)
                        .ToListAsync();
    }

    public async Task UpdateLog(FuelLog log)
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

    public async Task UpdateTransactionLog(FuelTransactionsRequest transaction,int previousReading,FuelLog log )
    {
        var result = await _context.FuelTransactions.SingleOrDefaultAsync(l => l.Id == Guid.Parse(transaction.Id));
        var fuelDateTime = transaction.FuelDate.MergeAndConvert(
                                        transaction.FuelTime.ConvertToDateTime()!.Value.ToLongTimeString());
        if(result != null) {
            if(transaction.LogType == EnumLogType.Dispense.ToString() && result.IsLessThanPrevious(transaction.Reading)) {
                result.Reading = transaction.Reading;
            }

            result.AssetCode = transaction.AssetCode;
            result.Driver = transaction.OperatorDriver;
            result.Quantity = transaction.Quantity;
            _context.Entry(result).State = EntityState.Modified;
        }        
        else {
            Guid guid = string.IsNullOrEmpty(transaction.Id) ? Guid.NewGuid() : Guid.Parse(transaction.Id);
            result = new FuelTransaction(guid,
                            transaction.AssetCode,
                            previousReading,
                            transaction.Reading,
                            transaction.OperatorDriver,
                            log.StationCode,
                            fuelDateTime,
                            transaction.Quantity);
            result.FuelLog = log;
            result.LogType = transaction.LogType;
            _context.Entry(result).State = EntityState.Added;
        }
    }
}
