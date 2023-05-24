namespace DemoApp.ValueTypes;

public class ProductId : ShortId<ProductId>, IShortId
{
    public static string Identifier => "prd";

    // public static InvoiceId Create(Guid guid) => new InvoiceId(guid);
    public ProductId()
        : base(System.Guid.NewGuid()) { }

    public ProductId(System.Guid guid)
        : base(guid) { }

    public ProductId(string shortid)
        : base(Parse(shortid)) { }
}
