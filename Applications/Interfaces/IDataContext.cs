using Core.PMV.Alerts;
using Core.PMV.Assets;
using Core.PMV.Fuels;
using Core.PMV.LogSheets;

namespace Applications.Interfaces;

public interface IDataContext : IDisposable
{
    public DbSet<LogSheet> LogSheets { get; set; }
    public DbSet<FuelLog> FuelLogs { get; set; }
    public DbSet<FuelTransaction> FuelTransactions { get; set; }
    public DbSet<LogSheetDetail> LogSheetDetails { get; set; }

    public DbSet<ServiceAlert> ServiceAlert { get; set; }
    public DbSet<ServiceAlertDetail> ServiceAlertDetails { get; set; }

    public DbSet<AssetRecord> AssetRecord { get; set; }

    public DbSet<ServiceLog> ServiceLogs { get; set; }

    

}