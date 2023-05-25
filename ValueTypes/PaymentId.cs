namespace DemoApp.ValueTypes;

public class PaymentId : ShortId<PaymentId>, IShortId
{
    public static string Identifier => "pym";

    public PaymentId()
        : base(System.Guid.NewGuid()) { }

    public PaymentId(System.Guid guid)
        : base(guid) { }

    public PaymentId(string shortid)
        : base(Parse(shortid)) { }
}
