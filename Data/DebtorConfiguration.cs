using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DebtorConfiguration : IEntityTypeConfiguration<Debtor>
{
    public void Configure(EntityTypeBuilder<Debtor> builder)
    {
        builder.ToTable("Debtors");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        // builder.Property(x => x.Id).HasConversion(x => x.Value, x => new DebtorId(x));
        builder.HasDiscriminator<int>("DebtorType").HasValue<Person>(1).HasValue<Company>(2);
        builder.HasMany(x => x.History).WithOne(x => x.Debtor).HasForeignKey(x => x.DebtorId);
    }
}
