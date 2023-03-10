using Applications.Interfaces;
using Applications.UseCases.PMV.Assets.Interfaces;
using Applications.UseCases.PMV.Fuels.Interfaces;
using Applications.UseCases.PMV.ServiceAlerts.Interfaces;
using Core.SeedWorks.Attributes;
using Infrastructure.Context.Db;
using Infrastructure.Services.PMV.Repositories;

namespace Infrastructure.Services.PMV;

public class UnitWork : IUnitWork
{

    private readonly PMVDataContext _context;
    
    private IServiceAlertRepository _serviceAlert;
    private IAssetRepository _assets;
    private IFuelLogRepository _logs;

    public UnitWork(IDataContext dataContext)
    {
        _context = (PMVDataContext)dataContext;
    }
    
   
    
    public IServiceAlertRepository ServiceAlert { 
        get {
            if(_serviceAlert == null)  _serviceAlert = new ServiceAlertRepository(_context);
            return _serviceAlert;
        } 
    }

    public IAssetRepository Assets {
        get {
          if(_assets == null)  _assets = new AssetRepository(_context);
            return _assets;
        }
    }

    public IFuelLogRepository FuelLogs {
        get {
            if(_logs == null) _logs = new FuelLogRepository(_context);
            return _logs;
        }
    }

    public async Task CommitSaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task CommitSaveAsync(string userOrgId)
    {   
        foreach(var entry in _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || 
                e.State == EntityState.Modified)) {

            

            if (entry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0) {
                if(entry.State == EntityState.Added) {
                    if(entry.Property("CreatedAt") != null) {
                        entry.Property("CreatedBy").CurrentValue = userOrgId;
                        entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                         entry.Property("UpdatedBy").CurrentValue = userOrgId;
                        entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    }
                }
                else if(entry.State == EntityState.Modified) {
                    if(entry.Property("UpdatedAt") != null) { 
                        entry.Property("UpdatedBy").CurrentValue = userOrgId;
                        entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    }
                }
            }
                
        }

        await _context.SaveChangesAsync(); 
    }

    public void Dispose() => _context.Dispose();

    public IDataContext GetContext()
    {
        return _context;
    }
}