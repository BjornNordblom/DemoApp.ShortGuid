// Mapper declaration
using Riok.Mapperly.Abstractions;

public interface IDomainMapper
{
    ClaimGetResponse MapClaimToClaimGetResponse(Claim claim);
    MoneyDto MapMoneyToMoneyDto(Money money);
}

[Mapper]
public partial class DomainMapper : IDomainMapper
{
    public partial ClaimGetResponse MapClaimToClaimGetResponse(Claim claim);

    public partial MoneyDto MapMoneyToMoneyDto(Money money);
}

public class Car
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class CarDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}

[Mapper]
public partial class CarMapper
{
    public partial CarDto CarToCarDto(Car car);
}
