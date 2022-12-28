using Core.PMV.Alerts;
using Core.PMV.Assets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations;

public class ServiceAlertConfig : IEntityTypeConfiguration<ServiceAlert>
{
    public void Configure(EntityTypeBuilder<ServiceAlert> builder)
    {
        builder.ToTable("ServiceAlerts");
    }
}

public class ServiceAlertDetailConfig : IEntityTypeConfiguration<ServiceAlertDetail>
{
    public void Configure(EntityTypeBuilder<ServiceAlertDetail> builder)
    {
        builder.ToTable("ServiceAlertDetails");
        builder.HasOne(s => s.ServiceAlert)
            .WithMany(s => s.Details);
    }
}

public class AssetRecordConfig : IEntityTypeConfiguration<AssetRecord>
{
    public void Configure(EntityTypeBuilder<AssetRecord> builder)
    {
        builder.ToTable("AssetRecords");

    }
}

public class ServiceLogConfig : IEntityTypeConfiguration<ServiceLog>
{
    public void Configure(EntityTypeBuilder<ServiceLog> builder)
    {
        builder.ToTable("ServiceLogs");
    }
}