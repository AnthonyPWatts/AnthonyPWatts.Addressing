using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class SpainAddressingIntegrationTests
{
    [Fact]
    public void AddSpainAddressing_RegistersSpanishValidatorWithoutStartupActions()
    {
        var services = new ServiceCollection();

        services.AddAddressing();
        services.AddSpainAddressing();

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var address = new Address(
            "Calle Mayor 1",
            null,
            "Madrid",
            "Madrid",
            new PostalCode("28013", CountryCode.Parse("ES")),
            CountryCode.Parse("ES"));

        factory.GetValidator(CountryCode.Parse("ES")).Validate(address);
    }
}
