

using Applications.Interfaces;
using Core.PMV.Alerts;
using Core.PMV.Assets;
using Core.PMV.LogSheets;
using Core.PMV.Stations;
using Infrastructure.Context.Configurations;

namespace Infrastructure.Context.Db;

public class PMVDataContext : DbContext, IDataContext
{
    public DbSet<LogSheet> LogSheets { get; set; }
    public DbSet<LogSheetDetail> LogSheetDetails { get; set; }
    public DbSet<ServiceAlert> ServiceAlert { get; set; }
    public DbSet<ServiceAlertDetail> ServiceAlertDetails { get; set; }
    public DbSet<AssetRecord> AssetRecord { get; set; }
    public DbSet<ServiceLog> ServiceLogs { get ;set; }

    public PMVDataContext(DbContextOptions<PMVDataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("HLMPMV");
        modelBuilder.ApplyConfiguration(new LogSheetConfig());
        modelBuilder.ApplyConfiguration(new LogSheetDetailConfig());
        modelBuilder.ApplyConfiguration(new ServiceAlertConfig());
        modelBuilder.ApplyConfiguration(new ServiceAlertDetailConfig());
        modelBuilder.ApplyConfiguration(new AssetRecordConfig());
        modelBuilder.ApplyConfiguration(new ServiceLogConfig());
        
        
        
        
    }
}