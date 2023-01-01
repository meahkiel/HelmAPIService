using Applications.UseCases.PMV.ServiceAlerts.Interfaces;
using Core.PMV.Alerts;
using Infrastructure.Context.Db;

namespace Infrastructure.Services.PMV.Repositories;

public class ServiceAlertRepository : IServiceAlertRepository
{
    private readonly PMVDataContext _context;

    public ServiceAlertRepository(PMVDataContext context)
    {
        _context = context;
    }
    public void Add(ServiceAlert value)
    {
        _context.ServiceAlert.Add(value);
    }

    /***
    
    ***/
    public async Task<ServiceAlert?> GetAlertServiceSetup(string category)
    {

        //check if there is a category
        var serviceAlert = _context.ServiceAlert
                                .Include(s => s.Details)
                                .Where(s => s.Category.Contains(category));

        if(serviceAlert != null) {
            return await serviceAlert.FirstOrDefaultAsync();
        }
        else {
            return await _context.ServiceAlert
                            .Include(s => s.Details)
                            .Where(s => s.Assigned == "all")
                            .FirstOrDefaultAsync();
        }
    }

    public async Task<IEnumerable<ServiceAlert>> GetAll()
    {
        return await _context.ServiceAlert
            .Where(s => s.InActive == false)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public Task<ServiceAlert?> GetByIdAsync(Guid Id)
    {
        return _context.ServiceAlert
                    .Include(s => s.Details)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(s => s.Id == Id);
    }

    public async Task<bool> IsGroupExists(string groupName)
    {
       return await _context.ServiceAlert
                        .AnyAsync(s => s.GroupName == groupName);
    }

    public void Update(ServiceAlert value)
    {
        if(value.Details.Count() > 0) {
            foreach(var detail in value.Details) {
                if(detail.Tracker == "a")
                    _context.ServiceAlertDetails.Add(detail);
                else if(detail.Tracker == "d") 
                    _context.ServiceAlertDetails.Remove(detail);
                else
                    _context.ServiceAlertDetails.Update(detail);
            }
        }
    }
}