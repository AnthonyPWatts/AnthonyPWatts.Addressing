using Addressing.Validation.Validators;

namespace Addressing.Tests;

public class CanadianAddressValidatorTests
{
    private readonly CanadianAddressValidator _validator = new CanadianAddressValidator();

    [Fact]
    public void Validate_ValidCanadianAddress_ShouldPass()
    {
        var address = new Address(
            "24 Sussex Dr",
            null,
            "Ottawa",
            "ON",
            new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_InvalidPostalCode_ShouldThrowException()
    {
        var address = new Address(
            "24 Sussex Dr",
            null,
            "Ottawa",
            "ON",
            new PostalCode("INVALID", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_InvalidProvince_ShouldThrowException()
    {
        var address = new Address(
            "24 Sussex Dr",
            null,
            "Ottawa",
            "ZZ",
            new PostalCode("K1A 0A1", CountryCode.Parse("CA")),
            CountryCode.Parse("CA"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
