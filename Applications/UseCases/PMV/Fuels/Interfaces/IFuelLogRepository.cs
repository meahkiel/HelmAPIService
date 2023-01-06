using Applications.UseCases.PMV.Fuels.DTO;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Interfaces;

public interface IFuelLogRepository
{
    Task<FuelLog> CreateLog(string stationCode,
        int referenceNo,
        DateTime? shiftStartTime,
        DateTime? shiftEndTime,
        int startShiftTankerKm,
        int endShiftTankerKm,
        float adjustedMeter,
        float adjustedBalance);

    Task<FuelLog?> GetLog(string id);

    Task<IEnumerable<FuelLog>> GetDraftLogs(string station);

    Task<IEnumerable<FuelTransactionReport>> GetTransactions(string dateFrom, string dateTo);

    Task UpdateLog(FuelLog log);
}
