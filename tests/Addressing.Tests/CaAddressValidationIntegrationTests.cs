using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class CaAddressValidationIntegrationTests
{
    [Fact]
    public void AddAddressing_WithCa_AllowsValidationOfCaAddress()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "ON",
            new PostalCode("K1A 0A9", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        factory.GetValidator(address.CountryCode).Validate(address);
    }
}
