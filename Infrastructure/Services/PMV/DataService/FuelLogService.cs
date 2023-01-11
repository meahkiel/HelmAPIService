using Applications.Interfaces;
using Applications.UseCases.PMV.Fuels.DTO;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Core.PMV.Fuels;
using Dapper;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.DataService;

public class FuelLogService : IFuelLogService
{
    private readonly PMVDataContext _context;

    public FuelLogService(IDataContext context)
    {
        _context = (PMVDataContext)context;
    }

    public async Task<IEnumerable<FuelLogMaster>> GetFuelLogMasterList(DateTime dateFrom, DateTime dateTo)
    {
        var results = await _context.Database.GetDbConnection().QueryAsync<FuelLogMaster, FuelDetailMaster,FuelLogMaster>(
                "sp_FuelLogSummary",
                (fuelLog,detail) => {
                    fuelLog.FuelTransactions.Add(detail);
                    return fuelLog;
                },
                splitOn: "TransactionId",
                param: new {
                    dateFrom = DateTime.Parse(dateFrom.ToShortDateString() + " 00:00") , 
                    dateTo = DateTime.Parse(dateTo.ToShortDateString() + " 23:59:59") },
                commandType: System.Data.CommandType.StoredProcedure);

        ICollection<FuelLogMaster> masters = new List<FuelLogMaster>();

        foreach(FuelLogMaster result in results) {
            if(!masters.Any(m => m.Id == result.Id)) {
                masters.Add(result);
            }
            else {
                var master = masters.SingleOrDefault(m => m.Id == result.Id);
                master!.FuelTransactions.Add(result.FuelTransactions.SingleOrDefault()!);
            }
        }

        return masters;
    }

    public async Task<FuelTransaction> GetLatestFuelLogRecord(string assetCode, int currentReading)
    {
        var previousRecord = await _context.FuelTransactions.Where(d => d.AssetCode == assetCode)
                                   .OrderByDescending(l => l.Reading)
                                   .OrderByDescending(l => l.FuelDateTime)
                                   .FirstOrDefaultAsync() ?? new FuelTransaction();

        if (!previousRecord.IsLessThan(currentReading))
            throw new Exception("Current reading must be higher than the previous SMU");

        return previousRecord;
    }
}