

using Applications.Interfaces;
using Core.PMV.Alerts;
using Core.PMV.LogSheets;
using Core.PMV.Stations;
using Infrastructure.Context.Configurations;

namespace Infrastructure.Context.Db;

public class PMVDataContext : DbContext, IDataContext
{
    public DbSet<LogSheet> LogSheets { get; set; }
    public DbSet<LogSheetDetail> LogSheetDetails { get; set; }
    public DbSet<ServiceAlert> ServiceAlert { get; set; }
    public DbSet<LVStation> LVStations { get; set; }


    public PMVDataContext(DbContextOptions<PMVDataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("PMV");
        modelBuilder.ApplyConfiguration(new LogSheetConfig());
        modelBuilder.ApplyConfiguration(new LogSheetDetailConfig());
        
        modelBuilder.Entity<ServiceAlert>().ToTable("ServiceAlerts");
        modelBuilder.Entity<ServiceAlertDetail>().ToTable("ServiceAlertDetails");
        modelBuilder.Entity<ServiceAlertDetail>().HasOne(s => s.ServiceAlert)
            .WithMany(s => s.Details);
        
    }


}