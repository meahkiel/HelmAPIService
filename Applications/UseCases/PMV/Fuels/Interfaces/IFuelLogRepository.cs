using Applications.UseCases.PMV.Fuels.DTO;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Interfaces;

public interface IFuelLogRepository
{
    void AddFuelLog(FuelLog log);

    Task RemoveTransactions(string[] ids);

    Task<FuelLog?> GetLog(string id);
    Task<FuelLog> GetSingleLog(string id);

    Task<IEnumerable<FuelLog>> GetDraftLogs(string station);
    
    Task<IEnumerable<FuelLog>> GetTankTransactions(string fuelStation,DateTime dateFrom, DateTime dateTo);

    Task UpdateLog(FuelLog log);

    void AddTransactionLog(FuelTransaction transaction);
    void UpdateTransactionLog(FuelTransaction transaction);
}
