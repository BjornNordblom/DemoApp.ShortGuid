public record Product
{
    public ProductId Id { get; init; } = null!;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Money Price { get; init; } = null!;
    public int Quantity { get; init; }
}
