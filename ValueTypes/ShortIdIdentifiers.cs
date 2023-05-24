namespace DemoApp.ValueTypes;

public static class ShortIdIdentifiers
{
    public static Dictionary<string, Type> All =>
        new()
        {
            { InvoiceId.Identifier, typeof(InvoiceId) },
            { ClaimId.Identifier, typeof(ClaimId) },
        };
}
