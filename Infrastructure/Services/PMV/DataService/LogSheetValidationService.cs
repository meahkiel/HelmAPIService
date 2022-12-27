using Applications.Interfaces;
using Applications.UseCases.PMV.Services;
using Core.PMV.LogSheets;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.DataService
{
    public class LogSheetValidationService : ILogSheetValidationService
    {

        private readonly PMVDataContext _context;

        public LogSheetValidationService(IDataContext context)
    {
        _context = (PMVDataContext)context;
    }
        public async Task<LogSheetDetail> GetLatestFuelLogRecord(string assetCode,int currentReading)
        {
            var previousRecord = await _context.LogSheetDetails.Where(d => d.AssetCode == assetCode)
                                    .OrderByDescending(l => l.Quantity)
                                    .OrderByDescending(l => l.FuelTime)
                                    .FirstOrDefaultAsync() ?? new LogSheetDetail();

            if(!previousRecord.IsLessThan(currentReading)) 
                throw new Exception("Current reading must be higher than the previous SMU");

            return previousRecord;
        }
    }
}