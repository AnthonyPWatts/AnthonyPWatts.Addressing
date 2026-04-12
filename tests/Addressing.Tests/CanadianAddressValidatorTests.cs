using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class CanadianAddressValidatorTests
{
    private readonly CanadianAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_DoesNotThrow()
    {
        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "ON",
            new PostalCode("K1A 0A9", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_WithInvalidProvince_Throws()
    {
        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "XX",
            new PostalCode("K1A 0A9", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
