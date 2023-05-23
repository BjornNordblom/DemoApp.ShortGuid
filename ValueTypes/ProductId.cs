using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;

[TypeConverter(typeof(ProductIdTypeConverter))]
[JsonConverter(typeof(ProductIdJsonConverter))]
public record ProductId
{
    private static string Prefix => "prd";
    private readonly ShortGuid _value;

    public Guid Guid => _value;

    public ProductId(ShortGuid Value)
    {
        _value = Value;
    }

    public static implicit operator Guid(ProductId shortGuid) => shortGuid._value;

    public static implicit operator String(ProductId productId)
    {
        return $"{ProductId.Prefix}:{productId._value.ToString()}";
    }

    public override string ToString()
    {
        return $"{ProductId.Prefix}:{_value.ToString()}";
    }

    public static ProductId Parse(string input)
    {
        var indexOfSplit = input.IndexOf(':');
        if (indexOfSplit < 0)
        {
            throw new ArgumentException(
                $"Invalid {nameof(ProductId)}, no type info, expected prefix {Prefix}: {input}"
            );
        }
        var prefix = input.Substring(0, indexOfSplit);
        if (prefix != Prefix)
        {
            throw new ArgumentException(
                $"Invalid {nameof(ProductId)}, expected prefix {Prefix}: {input}"
            );
        }
        input = input.Substring(indexOfSplit + 1);
        var parsedGuid = new Guid(WebEncoders.Base64UrlDecode(input.ToString()));
        return new ProductId(parsedGuid);
    }

    public static bool TryParse(string input, out ProductId? result)
    {
        if (input is null)
        {
            result = default;
            return false;
        }
        var indexOfSplit = input.IndexOf(':');
        if (indexOfSplit < 0)
        {
            result = default;
            return false;
        }
        var prefix = input.Substring(0, indexOfSplit);
        if (prefix != Prefix)
        {
            result = default;
            return false;
        }
        try
        {
            result = Parse(input);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    private sealed class ProductIdTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);

        public override object? ConvertFrom(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value
        ) => value is string str ? Parse(str) : null;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(string);

        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType
        ) => ((ShortGuid)value).ToString();
    }

    private sealed class ProductIdJsonConverter : JsonConverter<ProductId>
    {
        public override ProductId Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var str = reader.GetString();
            if (str != null)
                return Parse(str);

            return default;
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProductId value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(value.ToString());
    }
}
