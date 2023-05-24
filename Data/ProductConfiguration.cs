using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Valueconverter from ShortGuid to Guid

        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).HasConversion(v => v.Value, v => new ProductId(v));
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        builder.OwnsOne(
            p => p.Price,
            pm =>
            {
                pm.Property(x => x.Amount)
                    .HasColumnName("Price")
                    .IsRequired()
                    .HasColumnType("decimal(18,4)");
                pm.Property(x => x.Currency).HasColumnName("Currency").IsRequired().HasMaxLength(3);
            }
        );
        //https://stackoverflow.com/questions/50862525/seed-entity-with-owned-property
        builder.HasData(
            new
            {
                Id = new ProductId(Guid.Parse("679813ff-317b-4952-83dc-119c3848b35c")),
                Name = "Product 1",
                Description = "Product 1 Description"
            },
            new
            {
                Id = new ProductId(Guid.Parse("a7162438-b638-4bf1-a8c4-32fd9a1c498a")),
                Name = "Product 2",
                Description = "Product 2 Description"
            },
            new
            {
                Id = new ProductId(Guid.Parse("0118f3c9-205f-47a0-92c9-702a8ad21bb8")),
                Name = "Product 3",
                Description = "Product 3 Description"
            }
        );

        builder
            .OwnsOne(p => p.Price)
            .HasData(
                new
                {
                    ProductId = new ProductId(Guid.Parse("679813ff-317b-4952-83dc-119c3848b35c")),
                    Amount = 10.10M,
                    Currency = "EUR"
                },
                new
                {
                    ProductId = new ProductId(Guid.Parse("a7162438-b638-4bf1-a8c4-32fd9a1c498a")),
                    Amount = 20.20M,
                    Currency = "EUR"
                },
                new
                {
                    ProductId = new ProductId(Guid.Parse("0118f3c9-205f-47a0-92c9-702a8ad21bb8")),
                    Amount = 30.30M,
                    Currency = "EUR"
                }
            );
    }
}
