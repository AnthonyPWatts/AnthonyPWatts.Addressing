namespace ISOCodex.Addressing.Tests;

public class CountryCodeTests
{
    [Fact]
    public void Parse_WithValidCode_ReturnsCountryCode()
    {
        var result = CountryCode.Parse("gb");

        Assert.Equal("GB", result.Code);
    }

    [Fact]
    public void TryParse_WithInvalidCode_ReturnsFalse()
    {
        var success = CountryCode.TryParse("ZZZ", out var result);

        Assert.False(success);
        Assert.Equal(default, result);
    }

    [Fact]
    public void IsValid_WithValidAndInvalidCodes_ReturnsExpectedResult()
    {
        Assert.True(CountryCode.IsValid("US"));
        Assert.False(CountryCode.IsValid("USA"));
    }
}
