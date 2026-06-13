namespace ISOCodex.Addressing.Tests;

public class PostalCodeTests
{
    [Fact]
    public void Constructor_WithValidValues_SetsProperties()
    {
        var result = new PostalCode("CV1 1AA");

        Assert.Equal("CV1 1AA", result.Code);
    }

    [Fact]
    public void Constructor_WithBlankCode_Throws()
    {
        Assert.Throws<ArgumentException>(() => new PostalCode(" "));
    }
}
