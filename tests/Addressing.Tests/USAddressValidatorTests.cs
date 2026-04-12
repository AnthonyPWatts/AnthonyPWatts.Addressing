using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class USAddressValidatorTests
{
    private readonly USAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_DoesNotThrow()
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
    public void Validate_WithoutState_Throws()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            null,
            new PostalCode("20500", CountryCode.Parse("US")),
            CountryCode.Parse("US"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
