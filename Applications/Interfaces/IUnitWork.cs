using Applications.UseCases.PMV.Assets.Interfaces;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Applications.UseCases.PMV.ServiceAlerts.Interfaces;

namespace Applications.Interfaces;

public interface IUnitWork : IDisposable
{
    public ILogSheetRepository LogSheets {get;}
    public IFuelLogRepository FuelLogs {get;}
    public IServiceAlertRepository ServiceAlert { get; }

    public IAssetRepository Assets {get;}

    IDataContext GetContext();
     
    Task CommitSaveAsync();
    Task CommitSaveAsync(string userOrgId);        
}