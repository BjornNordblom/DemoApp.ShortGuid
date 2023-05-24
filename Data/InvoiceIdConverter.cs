using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DemoApp.ValueTypes;

public class InvoiceIdConverter : ValueConverter<InvoiceId, Guid>
{
    public InvoiceIdConverter()
        : base(v => v.Value, v => new InvoiceId(v)) { }
}
