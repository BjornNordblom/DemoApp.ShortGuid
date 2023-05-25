using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoicePaymentAllocationConfiguration
    : IEntityTypeConfiguration<InvoicePaymentAllocation>
{
    public void Configure(EntityTypeBuilder<InvoicePaymentAllocation> builder)
    {
        builder.ToTable("InvoicePaymentAllocation");
    }
}
