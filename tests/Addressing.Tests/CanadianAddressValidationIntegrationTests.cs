using System;
using Addressing.Validation;
using Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Addressing.Tests;

public class CanadianAddressValidationIntegrationTests
{
    [Fact]
    public void Validate_WithValidCanadianAddress_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: "ON",
            postalCode: new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("CA"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithValidCanadianAddressAndNoProvince_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: null,
            postalCode: new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("CA"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithNullAddress_ThrowsArgumentNullException()
    {
        var validator = new CanadianAddressValidator();

        var ex = Assert.Throws<ArgumentNullException>(() => validator.Validate(null));

        Assert.Equal("address", ex.ParamName);
        Assert.Contains("Address cannot be null.", ex.Message);
    }

    [Fact]
    public void Validate_WithWrongCountry_ThrowsArgumentException()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: "ON",
            postalCode: new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("CountryCode must be 'CA' for Canadian addresses.", ex.Message);
    }

    [Theory]
    [InlineData("K1A0A1")]
    [InlineData("k1a 0a1")]
    [InlineData("12345")]
    [InlineData("INVALID")]
    public void Validate_WithInvalidCanadianPostalCode_ThrowsArgumentException(string postcode)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: "ON",
            postalCode: new PostalCode(postcode, CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("CA"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("PostalCode must be a valid Canadian postal code", ex.Message);
    }

    [Theory]
    [InlineData("ZZ")]
    [InlineData("Ontario")]
    [InlineData("123")]
    public void Validate_WithInvalidProvinceOrTerritory_ThrowsArgumentException(string stateOrProvince)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: stateOrProvince,
            postalCode: new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("CA"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("StateOrProvince", ex.Message);
    }

    [Fact]
    public void Validate_WithLowercaseProvinceCode_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("CA"));

        using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("CA"));

        var address = new Address(
            line1: "24 Sussex Dr",
            line2: null,
            city: "Ottawa",
            stateOrProvince: "on",
            postalCode: new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            countryCode: CountryCode.Parse("CA"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }
}
