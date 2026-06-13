using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class GBAddressValidatorTests
{
    private readonly GBAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_ReturnsValidResult()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA"),
            CountryCode.GB);

        var result = _validator.Validate(address);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithLowercasePostcodeWithoutSpace_ReturnsValidResult()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("sw1a2aa"),
            CountryCode.GB);

        var result = _validator.Validate(address);

        Assert.True(result.IsValid);
        Assert.Equal("sw1a2aa", address.PostalCode.Code);
    }

    [Fact]
    public void Validate_WithInvalidPostcode_ReturnsIssue()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("BADCODE"),
            CountryCode.GB);

        var result = _validator.Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.PostalCode.Invalid");
    }
}
