using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItems");
        builder.HasKey(x => x.InvoiceId);
        builder.Property(x => x.Quantity).IsRequired();
        builder.HasOne(x => x.Invoice).WithMany(x => x.Items).HasForeignKey(x => x.InvoiceId);
        builder.OwnsOne(
            p => p.ProductPrice,
            pm =>
            {
                pm.Property(x => x.Amount)
                    .HasColumnName("Price")
                    .IsRequired()
                    .HasColumnType("decimal(18,4)");
                pm.Property(x => x.Currency).HasColumnName("Currency").IsRequired().HasMaxLength(3);
            }
        );
    }
}
