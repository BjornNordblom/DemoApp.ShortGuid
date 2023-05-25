using System.ComponentModel.DataAnnotations;
using DemoApp.ValueTypes;

public class Payment
{
    [Key]
    public PaymentId Id { get; init; } = default!;
    public Money Amount { get; init; } = default!;

    public ICollection<PaymentAllocation> PaymentAllocations { get; init; } = default!;
}

public class PaymentAllocation
{
    [Key]
    public PaymentAllocationId Id { get; init; } = default!;
    public PaymentId PaymentId { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
    public string Discriminator { get; init; } = default!;
    public decimal PlacedAmount { get; init; } = default!;
}

public class InvoicePaymentAllocation : PaymentAllocation
{
    public InvoiceId InvoiceId { get; set; } = default!;
    public Invoice Invoice { get; set; } = default!;
}
