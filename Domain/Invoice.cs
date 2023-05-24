using DemoApp.ValueTypes;

public class Invoice
{
    public InvoiceId Id { get; init; } = default!;
    public ClaimId ClaimId { get; init; } = default!;
    public Claim Claim { get; init; } = default!;
    public string Number { get; init; } = default!;
    public DateTime IssueDate { get; init; } = default!;
    public DateTime DueDate { get; init; } = default!;
    public ContactPoint Customer { get; init; } = default!;
    public ContactPoint Delivery { get; init; } = default!;
    public string CustomerNotes { get; init; } = default!;
    public ICollection<InvoiceItem> Items { get; init; } = default!;
}
