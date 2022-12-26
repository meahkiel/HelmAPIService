using Applications.UseCases.PMV.LogSheets.Interfaces;

namespace Applications.Interfaces
{
    public interface IUnitWork
    {
        public ILogSheetRepository LogSheets {get;}

        IDataContext GetContext();
         
        Task SaveChangesAsyncRoot();
        Task SaveChangesAsyncRoot(string userOrgId);        
    }
}