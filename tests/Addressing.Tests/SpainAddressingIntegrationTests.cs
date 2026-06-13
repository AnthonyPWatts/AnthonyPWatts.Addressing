using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Formatting;
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
            new PostalCode("28013"),
            CountryCode.ES);

        var result = factory.GetValidator(CountryCode.ES).Validate(address);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void AddSpainAddressing_ValidatorReturnsStructuredIssues()
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
            "Barcelona",
            new PostalCode("28013"),
            CountryCode.ES);

        var result = factory.GetValidator(CountryCode.ES).Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.PostalCode.ProvinceMismatch");
    }

    [Fact]
    public void AddSpainAddressing_RegistersSpanishFormatter()
    {
        var services = new ServiceCollection();

        services.AddAddressing();
        services.AddSpainAddressing();

        using var serviceProvider = services.BuildServiceProvider();
        var formatter = serviceProvider.GetRequiredService<IAddressFormatter>();

        var address = new Address(
            "Calle Mayor 1",
            null,
            "Madrid",
            "Madrid",
            new PostalCode("28013"),
            CountryCode.ES);

        Assert.Equal(
            "Calle Mayor 1\n28013 Madrid\nSpain",
            formatter.Format(address));
    }
}
