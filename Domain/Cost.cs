using DemoApp.ValueTypes;

public class Cost
{
    public Guid Id { get; set; }
    public ClaimId ClaimId { get; set; } = default!;
    public Claim Claim { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
}
