using DemoApp.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ProductIdConverter : ValueConverter<ProductId, Guid>
{
    public ProductIdConverter()
        : base(v => v.Value, v => new ProductId(v)) { }
}
