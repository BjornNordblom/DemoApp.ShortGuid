using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PaymentIdConverter : ValueConverter<PaymentId, Guid>
{
    public PaymentIdConverter()
        : base(v => v.Value, v => new PaymentId(v)) { }
}
