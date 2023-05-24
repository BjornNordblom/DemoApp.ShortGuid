using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class CurrencyConverter : ValueConverter<Currency, string>
{
    public CurrencyConverter()
        : base(currency => currency.CurrencyCode, code => new Currency(code)) { }
}
