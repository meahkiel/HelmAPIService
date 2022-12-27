
using Core.PMV.Alerts;

namespace Applications.UseCases.PMV.ServiceAlerts.Interfaces;

public interface IServiceAlertRepository : IRepository<ServiceAlert> 
{
    Task<bool> IsGroupExists(string groupName);

    
    
}