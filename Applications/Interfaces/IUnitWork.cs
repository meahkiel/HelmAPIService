using Applications.UseCases.PMV.LogSheets.Interfaces;
using Applications.UseCases.PMV.ServiceAlerts.Interfaces;

namespace Applications.Interfaces;

public interface IUnitWork
{
    public ILogSheetRepository LogSheets {get;}
    public IServiceAlertRepository ServiceAlert { get; set; }

    IDataContext GetContext();
     
    Task CommitSaveAsync();
    Task CommitSaveAsync(string userOrgId);        
}