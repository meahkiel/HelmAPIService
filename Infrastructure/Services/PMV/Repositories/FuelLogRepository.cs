using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;

public class FuelLogRepository : IFuelLogRepository
{
    private readonly PMVDataContext _context;

    public FuelLogRepository(PMVDataContext context)
    {
        _context = context;
    }
    public async Task<FuelLog> CreateLog(
        string stationCode,
        int referenceNo, 
        DateTime? shiftStartTime, 
        DateTime? shiftEndTime, 
        int startShiftTankerKm, 
        int endShiftTankerKm, 
        float adjustedMeter, 
        float adjustedBalance)
    {
        //get the latest record 
        FuelLog? record = await _context.FuelLogs
                            .Where(l => l.StationCode == stationCode)
                            .OrderByDescending(l => l.Date)
                            .OrderByDescending(l => l.ReferenceNo)
                            .FirstOrDefaultAsync();
        
        float openingMeter = 0f;
        float openingBalance = 0f;
        int lastDocumentNo = 0;
        
        if (record != null)
        {
            openingMeter = record.ClosingMeter;
            openingBalance = record.RemainingBalance;
            lastDocumentNo = record.DocumentNo;
        }


        var log = FuelLog.Create(
            stationCode,
            lastDocumentNo,
            referenceNo,
            shiftStartTime,
            shiftEndTime,
            startShiftTankerKm,
            endShiftTankerKm,
            openingMeter,
            openingBalance,
            adjustedBalance,
            adjustedMeter);

        _context.FuelLogs.Add(log);

        return log;

    }

    public Task<IEnumerable<FuelLog>> GetDraftLogs(string station)
    {
        throw new NotImplementedException();
    }

    public Task<FuelLog?> GetLog(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FuelTransactionReport>> GetTransactions(string dateFrom, string dateTo)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLog(FuelLog log)
    {
        throw new NotImplementedException();
    }
}
