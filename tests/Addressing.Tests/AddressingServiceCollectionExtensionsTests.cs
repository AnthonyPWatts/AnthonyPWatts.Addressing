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
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        Assert.IsType<GBAddressValidator>(validator);
    }

    [Fact]
    public void AddAddressing_WithUsGbCa_RegistersAllRequestedValidators()
    {
        var services = new ServiceCollection();

        services.AddAddressing(
            CountryCode.Parse("US"),
            CountryCode.Parse("GB"),
            CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        Assert.IsType<USAddressValidator>(factory.GetValidator(CountryCode.Parse("US")));
        Assert.IsType<GBAddressValidator>(factory.GetValidator(CountryCode.Parse("GB")));
        Assert.IsType<CAAddressValidator>(factory.GetValidator(CountryCode.Parse("CA")));
    }

    [Fact]
    public void AddAddressing_WithUnsupportedCountry_ThrowsArgumentExceptionWhenFactoryIsResolved()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("ES"));

        using var serviceProvider = services.BuildServiceProvider();

        var ex = Assert.Throws<ArgumentException>(
            () => serviceProvider.GetRequiredService<IAddressValidatorFactory>());

        Assert.Contains("ES", ex.Message);
    }
}
