using DemoApp.ValueTypes;

public record ClaimGetRequest
{
    public ClaimId Id { get; init; } = default!;
}
