using Applications.Interfaces;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.DataService;

public class FuelLogService : IFuelLogService
{
    private readonly PMVDataContext _context;

    public FuelLogService(IDataContext context)
    {
        _context = (PMVDataContext)context;
    }


    public async Task<FuelTransaction> GetLatestFuelLogRecord(string assetCode, int currentReading)
    {
        var previousRecord = await _context.FuelTransactions.Where(d => d.AssetCode == assetCode)
                                   .OrderByDescending(l => l.Quantity)
                                   .OrderByDescending(l => l.FuelDateTime)
                                   .FirstOrDefaultAsync() ?? new FuelTransaction();

        if (!previousRecord.IsLessThan(currentReading))
            throw new Exception("Current reading must be higher than the previous SMU");

        return previousRecord;
    }
}