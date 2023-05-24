public struct Currency
{
    private string currencyCode;

    public static implicit operator Currency(string code) => new Currency(code);

    public static implicit operator string(Currency currency) => currency.CurrencyCode;

    public Currency(string code)
    {
        code = code.ToUpperInvariant();
        if (!IsValidCurrencyCode(code))
        {
            throw new ArgumentException("Invalid currency code.");
        }

        currencyCode = code;
    }

    public string CurrencyCode
    {
        get { return currencyCode; }
        set
        {
            if (!IsValidCurrencyCode(value))
            {
                throw new ArgumentException("Invalid currency code.");
            }

            currencyCode = value;
        }
    }

    private static bool IsValidCurrencyCode(string code)
    {
        // Add your logic here to validate against a list of official currency codes
        // For example, you can use a predefined list or an API to validate the code.

        // In this example, we'll consider USD, EUR, and GBP as valid currency codes.
        string[] validCodes = { "USD", "EUR", "GBP", "SEK", "NOK", "DKK", "CFH" };

        return validCodes.Contains(code.ToUpper());
    }
}
