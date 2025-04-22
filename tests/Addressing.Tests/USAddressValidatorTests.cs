using Addressing.Validation.Validators;

namespace Addressing.Tests;

public class USAddressValidatorTests
{
    private readonly USAddressValidator _validator = new();

    [Fact]
    public void Validate_ValidUSAddress_ShouldPass()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            "DC",
            new PostalCode("20500", CountryCode.Parse("US")),
            CountryCode.Parse("US"));

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_InvalidPostalCode_ShouldThrowException()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            "DC",
            new PostalCode("ABCDE", CountryCode.Parse("US")),
            CountryCode.Parse("US"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_InvalidState_ShouldThrowException()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            "ZZ",
            new PostalCode("20500", CountryCode.Parse("US")),
            CountryCode.Parse("US"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
