using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class UsAddressValidationIntegrationTests
{
    [Fact]
    public void AddAddressing_WithUs_AllowsValidationOfUsAddress()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            "DC",
            new PostalCode("20500", CountryCode.Parse("US")),
            CountryCode.Parse("US"));

        factory.GetValidator(address.CountryCode).Validate(address);
    }
}
