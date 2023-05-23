using Mediator;

public record ProductCreateCommand(string Name, string Currency, decimal Price)
    : IRequest<ProductGetResponse>;
