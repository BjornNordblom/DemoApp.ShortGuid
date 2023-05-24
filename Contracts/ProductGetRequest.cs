using DemoApp.ValueTypes;

public record ProductGetRequest
{
    public ProductId Id { get; init; } = default!;
}
