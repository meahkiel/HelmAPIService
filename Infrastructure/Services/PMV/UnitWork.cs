using Applications.Interfaces;
using Applications.UseCases.PMV.LogSheets.Interfaces;
using Applications.UseCases.PMV.ServiceAlerts.Interfaces;

namespace Infrastructure.Services.PMV;

public class UnitWork : IUnitWork
{

    private readonly IDataContext _context;

    public UnitWork(IDataContext dataContext)
    {
        _context = dataContext;
    }
    
    public ILogSheetRepository LogSheets => throw new NotImplementedException();
    public IServiceAlertRepository ServiceAlert { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task CommitSaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task CommitSaveAsync(string userOrgId)
    {
        throw new NotImplementedException();
    }

    public IDataContext GetContext()
    {
        return _context;
    }
}