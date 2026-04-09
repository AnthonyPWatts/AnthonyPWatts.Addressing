using System;
using Addressing.Validation;
using Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Addressing.Tests;

public class UkAddressValidationIntegrationTests
{
    [Fact]
    public void Validate_WithValidUkAddress_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        var address = new Address(
            line1: "10 Downing Street",
            line2: null,
            city: "London",
            stateOrProvince: null,
            postalCode: new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            countryCode: CountryCode.Parse("GB"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithNullAddress_ThrowsArgumentNullException()
    {
        var validator = new UKAddressValidator();

        var ex = Assert.Throws<ArgumentNullException>(() => validator.Validate(null));

        Assert.Equal("address", ex.ParamName);
        Assert.Contains("Address cannot be null.", ex.Message);
    }

    [Fact]
    public void Validate_WithWrongCountry_ThrowsArgumentException()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        var address = new Address(
            line1: "10 Downing Street",
            line2: null,
            city: "London",
            stateOrProvince: null,
            postalCode: new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("CountryCode must be 'GB' for UK addresses.", ex.Message);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("SW1A2AA")]
    [InlineData("sw1a 2aa")]
    [InlineData("INVALID")]
    public void Validate_WithInvalidUkPostcode_ThrowsArgumentException(string postcode)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        var address = new Address(
            line1: "10 Downing Street",
            line2: null,
            city: "London",
            stateOrProvince: null,
            postalCode: new PostalCode(postcode, CountryCode.Parse("GB")),
            countryCode: CountryCode.Parse("GB"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("PostalCode must be a valid UK postcode", ex.Message);
    }

    [Fact]
    public void Validate_WithSpecialCaseGir0Aa_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("GB"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("GB"));

        var address = new Address(
            line1: "Somewhere",
            line2: null,
            city: "London",
            stateOrProvince: null,
            postalCode: new PostalCode("GIR 0AA", CountryCode.Parse("GB")),
            countryCode: CountryCode.Parse("GB"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }
}