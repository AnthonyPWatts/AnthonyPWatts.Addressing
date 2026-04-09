using System;
using Addressing.Validation;
using Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Addressing.Tests;

public class UsAddressValidationIntegrationTests
{
    [Fact]
    public void Validate_WithValidUsAddress_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: "DC",
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithValidUsAddressAndZipPlus4_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "350 Fifth Avenue",
            line2: null,
            city: "New York",
            stateOrProvince: "NY",
            postalCode: new PostalCode("10118-0110", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithNullAddress_ThrowsArgumentNullException()
    {
        var validator = new USAddressValidator();

        var ex = Assert.Throws<ArgumentNullException>(() => validator.Validate(null));

        Assert.Equal("address", ex.ParamName);
        Assert.Contains("Address cannot be null.", ex.Message);
    }

    [Fact]
    public void Validate_WithWrongCountry_ThrowsArgumentException()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: "DC",
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("CA"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("CountryCode must be 'US' for US addresses.", ex.Message);
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("123456")]
    [InlineData("1234A")]
    [InlineData("12345 6789")]
    [InlineData("12345_6789")]
    public void Validate_WithInvalidUsZipCode_ThrowsArgumentException(string postcode)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: "DC",
            postalCode: new PostalCode(postcode, CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("PostalCode must be a valid US ZIP code", ex.Message);
    }

    [Fact]
    public void Validate_WithNullStateOrProvince_ThrowsArgumentException()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: null,
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("StateOrProvince cannot be null or empty for US addresses.", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Validate_WithBlankStateOrProvince_ThrowsArgumentException(string stateOrProvince)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: stateOrProvince,
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("StateOrProvince cannot be null or empty for US addresses.", ex.Message);
    }

    [Theory]
    [InlineData("XX")]
    [InlineData("New York")]
    [InlineData("England")]
    [InlineData("123")]
    public void Validate_WithInvalidStateOrProvince_ThrowsArgumentException(string stateOrProvince)
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: stateOrProvince,
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var ex = Assert.Throws<ArgumentException>(() => validator.Validate(address));

        Assert.Contains("StateOrProvince", ex.Message);
    }

    [Fact]
    public void Validate_WithLowercaseStateCode_DoesNotThrow()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.Parse("US"));

        using var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
        var validator = factory.GetValidator(CountryCode.Parse("US"));

        var address = new Address(
            line1: "1600 Pennsylvania Avenue NW",
            line2: null,
            city: "Washington",
            stateOrProvince: "dc",
            postalCode: new PostalCode("20500", CountryCode.Parse("US")),
            countryCode: CountryCode.Parse("US"));

        var exception = Record.Exception(() => validator.Validate(address));

        Assert.Null(exception);
    }
}