using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DebtorIdConverter : ValueConverter<DebtorId, Guid>
{
    public DebtorIdConverter()
        : base(v => v.Value, v => new DebtorId(v)) { }
}
