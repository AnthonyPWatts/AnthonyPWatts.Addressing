namespace ISOCodex.Addressing.Tests;

public class PostalCodeTests
{
    [Fact]
    public void Constructor_WithValidValues_SetsProperties()
    {
        var country = CountryCode.Parse("GB");
        var result = new PostalCode("CV1 1AA", country);

        Assert.Equal("CV1 1AA", result.Code);
        Assert.Equal(country, result.Country);
    }

    [Fact]
    public void Constructor_WithBlankCode_Throws()
    {
        var country = CountryCode.Parse("GB");

        Assert.Throws<ArgumentException>(() => new PostalCode(" ", country));
    }
}
