using DemoApp.ValueTypes;

public class Claim
{
    public ClaimId Id { get; init; } = default!;
    public string ReferenceNumber { get; init; } = default!;
}
