using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class PaymentAllocationIdConverter : ValueConverter<PaymentAllocationId, Guid>
{
    public PaymentAllocationIdConverter()
        : base(id => id.Value, value => new PaymentAllocationId(value)) { }
}
