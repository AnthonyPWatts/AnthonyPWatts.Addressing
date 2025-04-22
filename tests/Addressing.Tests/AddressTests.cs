using Xunit;

namespace Addressing.Tests;

public class AddressTests
{
    [Fact]
    public void Constructor_MissingLine1_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Address(
                null!,
                null,
                "London",
                null,
                new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
                CountryCode.Parse("GB")));
    }

    [Fact]
    public void Constructor_MissingCity_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Address(
                "10 Downing St",
                null,
                null!,
                null,
                new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
                CountryCode.Parse("GB")));
    }

    [Fact]
    public void Constructor_MissingPostalCode_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Address(
                "10 Downing St",
                null,
                "London",
                null,
                new PostalCode("", CountryCode.Parse("GB")),
                CountryCode.Parse("GB")));
    }

    [Fact]
    public void Constructor_ValidAddress_ShouldCreateInstance()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.NotNull(address);
        Assert.Equal("10 Downing St", address.Line1);
        Assert.Equal("London", address.City);
    }
}
