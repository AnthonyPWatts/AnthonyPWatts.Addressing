using ISOCodex.Addressing.Validation;
using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class AddressValidationResultTests
{
    [Fact]
    public void Validate_WithValidAddress_ReturnsValidResult()
    {
        var validator = new GBAddressValidator();
        var address = new Address(
            "10 Downing Street",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA"),
            CountryCode.GB);

        var result = validator.Validate(address);

        Assert.True(result.IsValid);
        Assert.Empty(result.Issues);
    }

    [Fact]
    public void Validate_WithInvalidUsAddress_ReturnsMultipleIssues()
    {
        var validator = new USAddressValidator();
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            null,
            new PostalCode("BAD"),
            CountryCode.CA);

        var result = validator.Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.CountryCode.Invalid"
                && issue.PropertyName == nameof(Address.CountryCode));
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.PostalCode.Invalid"
                && issue.PropertyName == nameof(Address.PostalCode));
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.StateOrProvince.Required"
                && issue.PropertyName == nameof(Address.StateOrProvince));
    }

    [Fact]
    public void Validate_WithNullAddress_ReturnsRequiredIssue()
    {
        var validator = new GBAddressValidator();

        var result = validator.Validate(null);

        var issue = Assert.Single(result.Issues);
        Assert.False(result.IsValid);
        Assert.Equal("Address.Required", issue.Code);
    }

    [Fact]
    public void FactoryValidator_CanReturnStructuredValidationResult()
    {
        var factory = new AddressValidatorFactory();
        factory.RegisterValidator(CountryCode.US, new USAddressValidator());
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            null,
            new PostalCode("BAD"),
            CountryCode.US);

        var result = factory.GetValidator(CountryCode.US).Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.PostalCode.Invalid");
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.StateOrProvince.Required");
    }

}
