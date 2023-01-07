using Applications.UseCases.PMV.Fuels.DTO;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Interfaces;

public interface IFuelLogRepository
{
    void AddFuelLog(FuelLog log);

    Task<FuelLog?> GetLog(string id);

    Task<IEnumerable<FuelLog>> GetDraftLogs(string station);

    Task<IEnumerable<FuelTransactionReport>> GetTransactions(string dateFrom, string dateTo);

    void UpdateLog(FuelLog log);
}
