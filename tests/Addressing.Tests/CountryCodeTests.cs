using System;
using System.Linq;
using Xunit;

namespace Addressing.Tests;

public class CountryCodeTests
{
    [Fact]
    public void Parse_WithValidUppercaseCode_ReturnsCountryCode()
    {
        var result = CountryCode.Parse("GB");

        Assert.Equal("GB", result.Code);
        Assert.Equal("GB", result.ToString());
    }

    [Fact]
    public void Parse_WithValidLowercaseCode_NormalisesToUppercase()
    {
        var result = CountryCode.Parse("gb");

        Assert.Equal("GB", result.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ZZ")]
    [InlineData("G")]
    [InlineData("GBR")]
    [InlineData("12")]
    public void Parse_WithInvalidInput_ThrowsArgumentException(string input)
    {
        var ex = Assert.Throws<ArgumentException>(() => CountryCode.Parse(input));

        Assert.Contains("Invalid country code", ex.Message);
    }

    [Fact]
    public void TryParse_WithValidCode_ReturnsTrueAndParsedCountryCode()
    {
        var success = CountryCode.TryParse("US", out var result);

        Assert.True(success);
        Assert.Equal("US", result.Code);
    }

    [Fact]
    public void TryParse_WithValidLowercaseCode_ReturnsTrueAndNormalisedCountryCode()
    {
        var success = CountryCode.TryParse("ca", out var result);

        Assert.True(success);
        Assert.Equal("CA", result.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("ZZ")]
    [InlineData("GBR")]
    public void TryParse_WithInvalidInput_ReturnsFalseAndDefaultCountryCode(string input)
    {
        var success = CountryCode.TryParse(input, out var result);

        Assert.False(success);
        Assert.Equal(default, result);
        Assert.Null(result.Code);
    }

    [Theory]
    [InlineData("GB", true)]
    [InlineData("gb", true)]
    [InlineData("US", true)]
    [InlineData("ZZ", false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(null, false)]
    public void IsValid_ReturnsExpectedResult(string input, bool expected)
    {
        var result = CountryCode.IsValid(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EqualCodes_AreEqual()
    {
        var first = CountryCode.Parse("GB");
        var second = CountryCode.Parse("GB");

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
        Assert.True(first.Equals(second));
        Assert.True(first.Equals((object)second));
    }

    [Fact]
    public void EqualCodes_WithDifferentInputCasing_AreEqual()
    {
        var first = CountryCode.Parse("GB");
        var second = CountryCode.Parse("gb");

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
    }

    [Fact]
    public void DifferentCodes_AreNotEqual()
    {
        var first = CountryCode.Parse("GB");
        var second = CountryCode.Parse("US");

        Assert.NotEqual(first, second);
        Assert.False(first == second);
        Assert.True(first != second);
        Assert.False(first.Equals(second));
    }

    [Fact]
    public void EqualCodes_HaveSameHashCode()
    {
        var first = CountryCode.Parse("GB");
        var second = CountryCode.Parse("gb");

        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void DefaultCountryCodes_AreEqual()
    {
        var first = default(CountryCode);
        var second = default(CountryCode);

        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.False(first != second);
        Assert.Null(first.Code);
    }

    [Fact]
    public void All_ContainsKnownCountryCodes()
    {
        var all = CountryCode.All.ToList();

        Assert.Contains(all, c => c == CountryCode.Parse("GB"));
        Assert.Contains(all, c => c == CountryCode.Parse("US"));
        Assert.Contains(all, c => c == CountryCode.Parse("CA"));
    }

    [Fact]
    public void All_DoesNotContainDuplicates()
    {
        var all = CountryCode.All.ToList();

        Assert.Equal(all.Count, all.Distinct().Count());
    }

    [Fact]
    public void All_ContainsOnlyUppercaseCodes()
    {
        var all = CountryCode.All.ToList();

        Assert.All(all, c => Assert.Equal(c.Code, c.Code?.ToUpperInvariant()));
    }
}