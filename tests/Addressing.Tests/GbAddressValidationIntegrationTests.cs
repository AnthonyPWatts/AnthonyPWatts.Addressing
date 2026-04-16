using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class GbAddressValidationIntegrationTests
{
    [Fact]
    public void AddAddressing_WithGb_AllowsValidationOfGbAddress()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        factory.GetValidator(address.CountryCode).Validate(address);
    }
}
