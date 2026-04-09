using Addressing.Validation;
using Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Addressing.Tests;

public class AddressingServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAddressing_WithGb_RegistersFactoryThatCanResolveUkValidator()
    {
        var services = new ServiceCollection();

        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        Assert.NotNull(validator);
        Assert.IsType<UKAddressValidator>(validator);
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

        var usValidator = factory.GetValidator(CountryCode.Parse("US"));
        var gbValidator = factory.GetValidator(CountryCode.Parse("GB"));
        var caValidator = factory.GetValidator(CountryCode.Parse("CA"));

        Assert.IsType<USAddressValidator>(usValidator);
        Assert.IsType<UKAddressValidator>(gbValidator);
        Assert.IsType<CanadianAddressValidator>(caValidator);
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

    [Fact]
    public void AddAddressing_WithNoCountries_RegistersEmptyFactory()
    {
        var services = new ServiceCollection();

        services.AddAddressing();

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

        var ex = Assert.Throws<InvalidOperationException>(
            () => factory.GetValidator(CountryCode.Parse("GB")));

        Assert.Contains("GB", ex.Message);
    }

    [Fact]
    public void AddAddressing_RegistersIAddressValidatorFactory()
    {
        var services = new ServiceCollection();

        services.AddAddressing(CountryCode.Parse("GB"));        

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetService<IAddressValidatorFactory>();

        Assert.NotNull(factory);
    }
}