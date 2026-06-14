using System.Reflection;

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
    public void NamedValues_ReturnExpectedCountryCodes()
    {
        Assert.Equal("GB", CountryCode.GB.Code);
        Assert.Equal("US", CountryCode.US.Code);
        Assert.Equal("CA", CountryCode.CA.Code);
        Assert.Equal("ES", CountryCode.ES.Code);
        Assert.Equal("FR", CountryCode.FR.Code);
        Assert.Equal("IE", CountryCode.IE.Code);
    }

    [Fact]
    public void NamedValues_ExistForEveryValidCountryCode()
    {
        var properties = typeof(CountryCode)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(property => property.PropertyType == typeof(CountryCode))
            .ToDictionary(property => property.Name, property => property);

        foreach (var countryCode in CountryCode.All)
        {
            Assert.True(
                properties.TryGetValue(countryCode.Code, out var property),
                $"Expected CountryCode.{countryCode.Code} to be defined.");

            var propertyValue = Assert.IsType<CountryCode>(property.GetValue(null));

            Assert.Equal(countryCode, propertyValue);
        }
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
