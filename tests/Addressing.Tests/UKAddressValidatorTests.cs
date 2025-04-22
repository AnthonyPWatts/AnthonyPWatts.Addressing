using Addressing.Validation.Validators;

namespace Addressing.Tests;

public class UKAddressValidatorTests
{
    private readonly UKAddressValidator _validator = new();

    [Fact]
    public void Validate_ValidUKAddress_ShouldPass()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        _validator.Validate(address);
    }

    [Theory]
    [InlineData("SW1A 1AA")]
    [InlineData("EC1A 1BB")]
    [InlineData("W1A 0AX")]
    [InlineData("M1 1AE")]
    [InlineData("B33 8TH")]
    [InlineData("CR2 6XH")]
    [InlineData("DN55 1PT")]
    public void Validate_ValidPostcodes_ShouldPass(string postcode)
    {
        var address = new Address(
            "221B Baker Street",
            null,
            "London",
            null,
            new PostalCode(postcode, CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_InvalidPostalCode_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("INVALID", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_IncorrectCountryCode_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("US"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Theory]
    [InlineData("SW1A@2AA")]
    [InlineData("SW1A#2AA")]
    public void Validate_PostcodesWithSpecialCharacters_ShouldThrowException(string postcode)
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode(postcode, CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_PostcodeWithoutSpace_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_PostcodeWithExtraSpaces_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A  2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_LowercasePostcode_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("sw1a 2aa", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Theory]
    [InlineData("SW1")]
    [InlineData("SW1A 2AAA")]
    public void Validate_PostcodesWithInvalidLength_ShouldThrowException(string postcode)
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode(postcode, CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_ValidEdgeCasePostcode_ShouldPass()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("GIR 0AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        _validator.Validate(address);
    }
    [Fact]
    public void Validate_ValidUKPostcodeWithNonUKCountryCode_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("US"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_PostcodeWithLeadingOrTrailingSpaces_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode(" SW1A 2AA ", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

    [Fact]
    public void Validate_PostcodeWithMixedCase_ShouldThrowException()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("Sw1A 2aA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }

}

