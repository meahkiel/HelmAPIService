using Core.PMV.Alerts;
using Core.PMV.LogSheets;
using Core.PMV.Stations;

namespace Applications.Interfaces;

public interface IDataContext : IDisposable
{
    public DbSet<LogSheet> LogSheets { get; set; }
    public DbSet<LogSheetDetail> LogSheetDetails { get; set; }

    public DbSet<ServiceAlert> ServiceAlert { get; set; }

    

}