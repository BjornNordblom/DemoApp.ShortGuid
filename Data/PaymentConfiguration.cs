using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(new PaymentIdConverter()).ValueGeneratedNever();
        builder.OwnsOne(
            x => x.Amount,
            x =>
            {
                x.Property(x => x.Amount)
                    .HasColumnName("Amount")
                    .IsRequired()
                    .HasColumnType("decimal(18,4)");
                x.Property(x => x.Currency).HasColumnName("Currency").IsRequired().HasMaxLength(3);
            }
        );
        builder
            .HasMany(x => x.PaymentAllocations)
            .WithOne(x => x.Payment)
            .HasForeignKey(x => x.PaymentId);
    }
}
