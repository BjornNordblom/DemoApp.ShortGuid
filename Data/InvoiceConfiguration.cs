using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Claim).WithMany().HasForeignKey(x => x.ClaimId);
        //builder.Property(x => x.Id).HasConversion(v => v.Value, v => new ProductId(v));
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Number).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IssueDate).IsRequired();
        builder.Property(x => x.DueDate).IsRequired();
        //builder.Property(x => x.Customer).IsRequired().HasMaxLength(200);
        builder.OwnsOne(
            p => p.Customer,
            cp =>
            {
                cp.Property(x => x.Name)
                    .HasColumnName("CustomerName")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Address)
                    .HasColumnName("CustomerAddress")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.City)
                    .HasColumnName("CustomerCity")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Country)
                    .HasColumnName("CustomerCountry")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.State)
                    .HasColumnName("CustomerState")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.ZipCode)
                    .HasColumnName("CustomerZipCode")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Email)
                    .HasColumnName("CustomerEmail")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Phone)
                    .HasColumnName("CustomerPhone")
                    .IsRequired()
                    .HasMaxLength(200);
            }
        );
        builder.OwnsOne(
            p => p.Delivery,
            cp =>
            {
                cp.Property(x => x.Name)
                    .HasColumnName("DeliveryName")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Address)
                    .HasColumnName("DeliveryAddress")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.City)
                    .HasColumnName("DeliveryCity")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Country)
                    .HasColumnName("DeliveryCountry")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.State)
                    .HasColumnName("Deliverytate")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.ZipCode)
                    .HasColumnName("DeliveryZipCode")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Email)
                    .HasColumnName("DeliveryEmail")
                    .IsRequired()
                    .HasMaxLength(200);
                cp.Property(x => x.Phone)
                    .HasColumnName("DeliveryPhone")
                    .IsRequired()
                    .HasMaxLength(200);
            }
        );
        builder.Property(x => x.CustomerNotes).IsRequired().HasMaxLength(200);
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.InvoiceId);
    }
}
