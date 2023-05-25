namespace DemoApp.ValueTypes;

public class PaymentAllocationId : ShortId<PaymentAllocationId>, IShortId
{
    public static string Identifier => "pal";

    public PaymentAllocationId()
        : base(System.Guid.NewGuid()) { }

    public PaymentAllocationId(System.Guid guid)
        : base(guid) { }

    public PaymentAllocationId(string shortid)
        : base(Parse(shortid)) { }
}
