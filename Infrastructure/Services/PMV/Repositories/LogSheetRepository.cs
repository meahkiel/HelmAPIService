using Applications.UseCases.PMV.LogSheets.DTO;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Core.PMV.LogSheets;
using Dapper;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;


public class LogSheetRepository : ILogSheetRepository
{
    private readonly PMVDataContext _context;
    public LogSheetRepository(PMVDataContext context)
    {
        _context = context;
       
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

    public void Add(LogSheet value) => _context.LogSheets.Add(value);

    public async Task<IEnumerable<LogSheetResponse?>> GetDraftSheetsByStation(string station)
    {  
        return await _context.LogSheets
            .Include(l => l.Details)
            .AsNoTracking()
            .Where(s => s.StationCode == station 
                    && s.Post.IsPosted == false)
            .Select(sheet => new LogSheetResponse {
                Id = sheet.Id.ToString(),
                FueledDate = sheet.ShiftStartTime,
                ReferenceNo = sheet.ReferenceNo,
                ShiftStartTime = sheet.ShiftStartTime.ToShortTimeString(),
                ShiftEndTime = sheet.ShiftEndTime.HasValue ? sheet.ShiftEndTime.Value.ToShortTimeString() : null,
                StartShiftTankerKm = sheet.StartShiftTankerKm,
                EndShiftTankerKm = sheet.EndShiftTankerKm,
                StartShiftMeterReading = sheet.StartShiftMeterReading,
                EndShiftMeterReading = sheet.EndShiftMeterReading,
                Station = sheet.StationCode,
                Remarks = sheet.Remarks,
                Fueler = sheet.CreatedBy ?? "",
                Details = sheet.Details.Select(d => new LogSheetDetailResponse {
                            Id = d.Id.ToString(),
                            AssetCode = d.AssetCode,
                            FuelTime = d.FuelTime,
                            OperatorDriver = d.OperatorDriver,
                            Reading = d.Reading,
                            PreviousReading = d.PreviousReading,
                            Quantity = d.Quantity,
                            DriverQatarIdUrl = d.DriverQatarIdUrl
                        }).ToList()
            })
        .ToListAsync();
    }

    public async Task<LogSheet?> GetDraft(Guid id)
    {
        return await _context.LogSheets
            .Include(l => l.Details)
            .SingleAsync(l => l.Post.IsPosted == false && l.Id == id);
    }

    public async Task<LogSheet?> GetLatestRecord()
    {
        return await _context.LogSheets
                .OrderBy(l => l.CreatedAt)
                .OrderBy(l => l.ReferenceNo)
                .LastOrDefaultAsync();
    }

    

    public async Task<IEnumerable<FuelLogTransactionsResponse>> GetTransactions(string dateFrom, string dateTo) {

        var results = await _context.Database.GetDbConnection()
                .QueryAsync<FuelLogTransactionsResponse>(
                "sp_PMVFuel_DispenseLogSheet",
                new {  
                    dateFrom = $"{dateFrom} 00:00:00" , 
                    dateTo = $"{dateTo} 23:59:59"  
                },
                commandType: System.Data.CommandType.StoredProcedure);
        
        return results;
    }

    public void Update(LogSheet value)
    {  
        var newDetails = value.Details.Where(d => d.Track == "new").ToList();
        if(newDetails.Count > 0) {
            foreach (var newDetail  in newDetails) {
                _context.LogSheetDetails.Add(newDetail);
            }
        }

        var updateDetails = value.Details.Where(d => d.Track == "update").ToList();
        if(newDetails.Count > 0) {
            foreach (var updateDetail  in updateDetails) {
                _context.LogSheetDetails.Update(updateDetail);
            }
        }

        _context.LogSheets.Update(value);
    }
    public async Task<IEnumerable<FuelLogTransactionsResponse>> GetPostedTransactionsByAsset(string assetCode)
    {
        var results = await _context.Database.GetDbConnection()
                .QueryAsync<FuelLogTransactionsResponse>(
                "sp_PMVFuel_TransactionByAsset",
                new {  assetCode = assetCode},
                commandType: System.Data.CommandType.StoredProcedure);
        
        return results;
    }

    public async Task<LogSheet?> GetSingleLogSheet(Guid id)
    {
        return await _context.LogSheets
            .Include(l => l.Details)
            .SingleAsync(l => l.Id == id);
    }
}