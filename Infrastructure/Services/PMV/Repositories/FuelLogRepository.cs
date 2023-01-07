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

    public Task<IEnumerable<FuelTransactionReport>> GetTransactions(string dateFrom, string dateTo)
    {
        throw new NotImplementedException();
    }

    public void UpdateLog(FuelLog log)
    {
        var details = log.FuelTransactions.Where(d => d.Track == "new").ToList();
        if(details.Count > 0) {
            foreach (var detail  in details) {
                _context.FuelTransactions.Add(detail);
            }
        }

        var updateDetails = log.FuelTransactions.Where(d => d.Track == "update").ToList();
        if(updateDetails.Count > 0) {
            foreach (var updateDetail  in updateDetails) {
                _context.FuelTransactions.Update(updateDetail);
            }
        }
    }
}
