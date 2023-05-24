namespace DemoApp.ValueTypes;

public static class ShortIdIdentifiers
{
    public static Dictionary<string, Type> All =>
        new()
        {
            { InvoiceId.Identifier, typeof(InvoiceId) },
            { ProductId.Identifier, typeof(ProductId) }
        };
}
