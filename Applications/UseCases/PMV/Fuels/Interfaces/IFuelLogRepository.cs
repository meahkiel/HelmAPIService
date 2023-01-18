using Applications.UseCases.PMV.Fuels.DTO;
using Core.PMV.Fuels;

namespace Applications.UseCases.PMV.Fuels.Interfaces;

public interface IFuelLogRepository
{
    void AddFuelLog(FuelLog log);

    Task RemoveTransactions(string[] ids);


    void UpdateLog(FuelLog log);

    void AddTransactionLog(FuelTransaction transaction);
    void UpdateTransactionLog(FuelTransaction transaction);
    Task<FuelLog?> GetLog(string id);
    Task<FuelLog?> GetSingleLog(string id);
    
    Task<IEnumerable<FuelLog>> GetDraftLogs(string fueler);
    
    Task<IEnumerable<FuelLog>> GetTankTransactions(string fuelStation,DateTime dateFrom, DateTime dateTo);
    
    Task<FuelLog?> GetLastFuelLog(string station); 
}
