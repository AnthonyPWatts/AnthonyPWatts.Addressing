using ISOCodex.Addressing.Validation;
using ISOCodex.Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class AddressingServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAddressing_WithGb_RegistersFactoryThatCanResolveGbValidator()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.GB);

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.GB);

        Assert.IsType<GBAddressValidator>(validator);
    }

    [Fact]
    public void AddAddressing_WithUsGbCa_RegistersAllRequestedValidators()
    {
        var services = new ServiceCollection();

        services.AddAddressing(
            CountryCode.US,
            CountryCode.GB,
            CountryCode.CA);

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        Assert.IsType<USAddressValidator>(factory.GetValidator(CountryCode.US));
        Assert.IsType<GBAddressValidator>(factory.GetValidator(CountryCode.GB));
        Assert.IsType<CAAddressValidator>(factory.GetValidator(CountryCode.CA));
    }

    [Fact]
    public void AddAddressing_WithUnsupportedCountry_ThrowsArgumentException()
    {
        var services = new ServiceCollection();

        var ex = Assert.Throws<ArgumentException>(
            () => services.AddAddressing(CountryCode.ES));

        Assert.Contains("ES", ex.Message);
    }
}
