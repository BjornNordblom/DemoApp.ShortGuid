using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("Claims");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.ReferenceNumber).IsRequired();
        builder.Property(x => x.Currency).IsRequired();
        builder.HasMany(x => x.History).WithOne(x => x.Claim).HasForeignKey(x => x.ClaimId);
    }
}
