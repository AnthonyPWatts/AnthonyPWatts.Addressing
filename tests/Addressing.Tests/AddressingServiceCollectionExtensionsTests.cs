using ISOCodex.Addressing.Canada;
using ISOCodex.Addressing.GreatBritain;
using ISOCodex.Addressing.UnitedStates;
using ISOCodex.Addressing.Validation;
using ISOCodex.Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class AddressingServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAddressing_RegistersFactory()
    {
        var services = new ServiceCollection();
        services.AddAddressing();

        using var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetRequiredService<IAddressValidatorFactory>());
    }

    [Fact]
    public void CountryPacks_RegisterAllRequestedValidators()
    {
        var services = new ServiceCollection();

        services.AddAddressing();
        services.AddUnitedStatesAddressing();
        services.AddGreatBritainAddressing();
        services.AddCanadaAddressing();

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        Assert.IsType<USAddressValidator>(factory.GetValidator(CountryCode.US));
        Assert.IsType<GBAddressValidator>(factory.GetValidator(CountryCode.GB));
        Assert.IsType<CAAddressValidator>(factory.GetValidator(CountryCode.CA));
    }

    [Fact]
    public void AddAddressing_WithNoCountryPack_ThrowsWhenResolvingCountryValidator()
    {
        var services = new ServiceCollection();
        services.AddAddressing();

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var ex = Assert.Throws<InvalidOperationException>(
            () => factory.GetValidator(CountryCode.ES));

        Assert.Contains("ES", ex.Message);
    }

    [Fact]
    public void AddGenericAddressingFallbacks_RegistersFallbackValidator()
    {
        var services = new ServiceCollection();
        services.AddGenericAddressingFallbacks();

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("FR"));

        Assert.IsType<PermissiveAddressValidator>(validator);
    }

    [Fact]
    public void AddAddressing_WithGenericFallbacks_PrefersCountryValidator()
    {
        var services = new ServiceCollection();
        services.AddAddressing();
        services.AddGreatBritainAddressing();
        services.AddGenericAddressingFallbacks();

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        Assert.IsType<GBAddressValidator>(factory.GetValidator(CountryCode.GB));
        Assert.IsType<PermissiveAddressValidator>(factory.GetValidator(CountryCode.Parse("FR")));
    }
}
