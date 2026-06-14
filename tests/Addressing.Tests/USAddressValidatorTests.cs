using ISOCodex.Addressing.UnitedStates;

namespace ISOCodex.Addressing.Tests;

public class USAddressValidatorTests
{
    private readonly USAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_ReturnsValidResult()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            "DC",
            new PostalCode("20500"),
            CountryCode.US);

        var result = _validator.Validate(address);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithoutState_ReturnsIssue()
    {
        var address = new Address(
            "1600 Pennsylvania Avenue NW",
            null,
            "Washington",
            null,
            new PostalCode("20500"),
            CountryCode.US);

        var result = _validator.Validate(address);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Issues,
            issue => issue.Code == "Address.StateOrProvince.Required");
    }
}
