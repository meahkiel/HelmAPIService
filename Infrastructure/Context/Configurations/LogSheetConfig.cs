using Core.PMV.LogSheets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations;

public class LogSheetConfig : IEntityTypeConfiguration<LogSheet>
{
    public void Configure(EntityTypeBuilder<LogSheet> builder)
    {
        builder.ToTable("LogSheets");
    }
}


public class LogSheetDetailConfig : IEntityTypeConfiguration<LogSheetDetail>
{
    public void Configure(EntityTypeBuilder<LogSheetDetail> builder)
    {
        builder.ToTable("LogSheetDetails");
        builder.HasOne(d => d.LogSheet)
            .WithMany(l => l.Details);
    }
}