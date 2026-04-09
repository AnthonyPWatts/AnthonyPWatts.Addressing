using System;
using Xunit;

namespace Addressing.Tests;

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
    public void ToString_ReturnsCode()
    {
        var result = new PostalCode("20500", CountryCode.Parse("US"));

        Assert.Equal("20500", result.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhitespaceCode_ThrowsArgumentException(string code)
    {
        var country = CountryCode.Parse("GB");

        var ex = Assert.Throws<ArgumentException>(() => new PostalCode(code, country));

        Assert.Equal("code", ex.ParamName);
        Assert.Contains("Postal code cannot be null or empty.", ex.Message);
    }

    [Fact]
    public void EqualPostalCodes_AreEqual()
    {
        var country = CountryCode.Parse("GB");
        var first = new PostalCode("CV1 1AA", country);
        var second = new PostalCode("CV1 1AA", country);

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
        Assert.True(first.Equals(second));
        Assert.True(first.Equals((object)second));
    }

    [Fact]
    public void PostalCodes_WithDifferentCode_AreNotEqual()
    {
        var country = CountryCode.Parse("GB");
        var first = new PostalCode("CV1 1AA", country);
        var second = new PostalCode("CV2 1AA", country);

        Assert.NotEqual(first, second);
        Assert.False(first == second);
        Assert.True(first != second);
        Assert.False(first.Equals(second));
    }

    [Fact]
    public void PostalCodes_WithDifferentCountry_AreNotEqual()
    {
        var first = new PostalCode("12345", CountryCode.Parse("US"));
        var second = new PostalCode("12345", CountryCode.Parse("CA"));

        Assert.NotEqual(first, second);
        Assert.False(first == second);
        Assert.True(first != second);
        Assert.False(first.Equals(second));
    }

    [Fact]
    public void EqualPostalCodes_HaveSameHashCode()
    {
        var first = new PostalCode("20500", CountryCode.Parse("US"));
        var second = new PostalCode("20500", CountryCode.Parse("US"));

        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void DefaultPostalCodes_AreEqual()
    {
        var first = default(PostalCode);
        var second = default(PostalCode);

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
        Assert.Null(first.Code);
        Assert.Equal(default, first.Country);
    }

    [Fact]
    public void PostalCode_IsCaseSensitive()
    {
        var country = CountryCode.Parse("GB");
        var first = new PostalCode("ab12 3cd", country);
        var second = new PostalCode("AB12 3CD", country);

        Assert.NotEqual(first, second);
        Assert.False(first == second);
    }
}