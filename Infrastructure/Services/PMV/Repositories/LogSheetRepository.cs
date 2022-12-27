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
        _context.LogSheetDetails.Add(detail);
        
        return true;
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

    public async Task<IEnumerable<LogSheetResponse?>> GetDraftSheetsByStation(string station)
    {  
        return await _context.LogSheets
            .Include(l => l.Details)
            .Where(s => s.LvStationCode == station 
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
                LVStation = sheet.LvStationCode,
                Remarks = sheet.Remarks,
                EmployeeCode = sheet.CreatedBy ?? "",
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
                        .FirstOrDefaultAsync(l => l.Post.IsPosted == false && l.Id == id);
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