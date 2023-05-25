namespace DemoApp.ValueTypes;

public class DebtorId : ShortId<DebtorId>, IShortId
{
    public static string Identifier => "deb";

    public DebtorId()
        : base(System.Guid.NewGuid()) { }

    public DebtorId(System.Guid guid)
        : base(guid) { }

    public DebtorId(string shortid)
        : base(Parse(shortid)) { }
}
