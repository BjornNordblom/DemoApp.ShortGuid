namespace DemoApp.ValueTypes;

public class InvoiceId : ShortId<InvoiceId>, IShortId
{
    public static string Identifier => "inv";

    // public static InvoiceId Create(Guid guid) => new InvoiceId(guid);
    public InvoiceId()
        : base(System.Guid.NewGuid()) { }

    public InvoiceId(System.Guid guid)
        : base(guid) { }

    public InvoiceId(string shortid)
        : base(Parse(shortid)) { }
}
