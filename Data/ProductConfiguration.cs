using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Valueconverter from ShortGuid to Guid

        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).HasConversion(v => v.Value, v => new ProductId(v));
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Quantity).IsRequired();
        builder.OwnsOne(
            x => x.Price,
            b =>
            {
                b.Property(x => x.Amount)
                    .HasColumnName("Price")
                    .IsRequired()
                    .HasColumnType("decimal(18,4)");
                b.Property(x => x.Currency).HasColumnName("Currency").IsRequired().HasMaxLength(3);
            }
        );
    }
}
