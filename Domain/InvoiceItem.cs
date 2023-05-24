using DemoApp.ValueTypes;

public class InvoiceItem
{
    public InvoiceId InvoiceId { get; init; } = default!;
    public Invoice Invoice { get; init; } = default!;
    public ProductId ProductId { get; init; } = default!;
    public string ProductName { get; init; } = default!;
    public Money ProductPrice { get; init; } = default!;
    public int Quantity { get; init; } = default!;
}
