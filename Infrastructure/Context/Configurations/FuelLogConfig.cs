using Core.PMV.Fuels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations;

public class FuelLogConfig : IEntityTypeConfiguration<FuelLog>
{
    public void Configure(EntityTypeBuilder<FuelLog> builder)
    {
        builder.ToTable("FuelLog");
        builder.OwnsOne(l => l.Post);
        builder.HasMany(m => m.FuelTransactions)
                .WithOne(m => m.FuelLog);
    }
}

public class FuelTransactionConfig : IEntityTypeConfiguration<FuelTransaction>
{
    public void Configure(EntityTypeBuilder<FuelTransaction> builder)
    {
        builder.ToTable("FuelTransaction");
    }
}