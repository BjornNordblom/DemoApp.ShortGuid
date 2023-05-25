using DemoApp.ValueTypes;

public abstract class Debtor
{
    public DebtorId Id { get; init; } = default!;
    public ICollection<DebtorHistory> History { get; init; } = default!;
}

public class Person : Debtor
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
}

public class Company : Debtor
{
    public string Name { get; init; } = default!;
}
