using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class CAAddressValidatorTests
{
    private readonly CAAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_ReturnsValidResult()
    {
        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "ON",
            new PostalCode("K1A 0A9"),
            CountryCode.CA);

        var result = _validator.Validate(address);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithLowercasePostalCodeWithoutSpace_ReturnsValidResult()
    {
        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "ON",
            new PostalCode("k1a0a9"),
            CountryCode.CA);

        var result = _validator.Validate(address);

        Assert.True(result.IsValid);
        Assert.Equal("k1a0a9", address.PostalCode.Code);
    }

    [Fact]
    public void Validate_WithInvalidProvince_ReturnsIssue()
    {
        var address = new Address(
            "111 Wellington St",
            null,
            "Ottawa",
            "XX",
            new PostalCode("K1A 0A9"),
            CountryCode.CA);

        var result = _validator.Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.StateOrProvince.Invalid");
    }
}
