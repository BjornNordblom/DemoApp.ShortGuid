using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;

[TypeConverter(typeof(ShortGuidTypeConverter))]
[JsonConverter(typeof(ShortGuidJsonConverter))]
public readonly struct ShortGuid : IComparable<ShortGuid>, IEquatable<ShortGuid>
{
    private readonly Guid _value;
    private readonly string _prefix = string.Empty;

    public ShortGuid(Guid value) => _value = value;

    public static implicit operator Guid(ShortGuid shortGuid) => shortGuid._value;

    public static implicit operator ShortGuid(Guid guid) => new(guid);

    public static ShortGuid Parse(string input)
    {
        var indexOfSplit = input.IndexOf(':');
        if (indexOfSplit >= 0)
        {
            input = input.Substring(indexOfSplit + 1);
        }
        return new Guid(WebEncoders.Base64UrlDecode(input.ToString()));
    }

    public override string ToString()
    {
        Span<byte> bytes = stackalloc byte[16];
        _value.TryWriteBytes(bytes);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static bool TryParse(string input, out ShortGuid result)
    {
        if (input is null)
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

    int IComparable<ShortGuid>.CompareTo(ShortGuid other)
    {
        return _value.CompareTo(other._value);
    }

    bool IEquatable<ShortGuid>.Equals(ShortGuid other)
    {
        return _value.Equals(other._value);
    }

    private sealed class ShortGuidTypeConverter : TypeConverter
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

    private sealed class ShortGuidJsonConverter : JsonConverter<ShortGuid>
    {
        public override ShortGuid Read(
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
            ShortGuid value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(value.ToString());
    }
}
