using System;
using System.Linq;
using ISOCodex.Addressing.Profiles;
using ISOCodex.Addressing.Spain;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Tests;

public class AddressProfileProviderTests
{
    [Fact]
    public void GetProfile_WithRegisteredGbProfile_ReturnsCountrySpecificProfile()
    {
        var provider = new DefaultAddressProfileProvider();
        provider.RegisterProfile(
            CountryCode.GB,
            new AddressProfile(
                CountryCode.GB,
                new[]
                {
                    new AddressFieldProfile(
                        AddressField.AddressLine1,
                        "Address line 1",
                        true,
                        10)
                }));

        var profile = provider.GetProfile(CountryCode.GB);

        Assert.Equal(CountryCode.GB, profile.CountryCode);
        Assert.Equal(AddressProfileSource.CountrySpecific, profile.Source);
    }

    [Fact]
    public void AddAddressing_WithGb_RegistersProfileWithRequiredCoreFields()
    {
        var provider = BuildProfileProvider(CountryCode.GB);

        var profile = provider.GetProfile(CountryCode.GB);

        Assert.Equal(AddressProfileSource.CountrySpecific, profile.Source);
        AssertRequired(profile, AddressField.AddressLine1);
        AssertRequired(profile, AddressField.Locality);
        AssertRequired(profile, AddressField.PostalCode);
        AssertRequired(profile, AddressField.Country);
    }

    [Fact]
    public void AddAddressing_WithGb_LabelsPostalCodeAsPostcode()
    {
        var provider = BuildProfileProvider(CountryCode.GB);

        var postalCode = provider
            .GetProfile(CountryCode.GB)
            .Fields
            .Single(field => field.Field == AddressField.PostalCode);

        Assert.Equal("Postcode", postalCode.Label);
    }

    [Fact]
    public void AddAddressing_WithUs_UsesUsFieldLabels()
    {
        var provider = BuildProfileProvider(CountryCode.US);
        var profile = provider.GetProfile(CountryCode.US);

        Assert.Equal(
            "State",
            profile.Fields.Single(field => field.Field == AddressField.AdministrativeArea).Label);
        Assert.Equal(
            "ZIP code",
            profile.Fields.Single(field => field.Field == AddressField.PostalCode).Label);
    }

    [Fact]
    public void AddAddressing_WithCa_UsesCanadaFieldLabels()
    {
        var provider = BuildProfileProvider(CountryCode.CA);
        var profile = provider.GetProfile(CountryCode.CA);

        Assert.Equal(
            "Province or territory",
            profile.Fields.Single(field => field.Field == AddressField.AdministrativeArea).Label);
        Assert.Equal(
            "Postal code",
            profile.Fields.Single(field => field.Field == AddressField.PostalCode).Label);
    }

    [Fact]
    public void GetProfile_WithFallbackProfile_ReturnsGenericFallbackForRequestedCountry()
    {
        var services = new ServiceCollection();
        services.AddAddressing(CountryCode.GB);
        services.AddGenericAddressingFallbacks();

        using var serviceProvider = services.BuildServiceProvider();
        var provider = serviceProvider.GetRequiredService<IAddressProfileProvider>();

        var profile = provider.GetProfile(CountryCode.Parse("FR"));

        Assert.Equal(CountryCode.Parse("FR"), profile.CountryCode);
        Assert.Equal(AddressProfileSource.GenericFallback, profile.Source);
        AssertRequired(profile, AddressField.Country);
        Assert.Contains(
            profile.Fields,
            field => field.Field == AddressField.AddressLine1 && field.IsRequired);
    }

    [Fact]
    public void GetProfile_WhenNoProfileRegistered_ThrowsInvalidOperationException()
    {
        var provider = new DefaultAddressProfileProvider();

        var ex = Assert.Throws<InvalidOperationException>(
            () => provider.GetProfile(CountryCode.US));

        Assert.Contains("US", ex.Message);
    }

    [Fact]
    public void GetProfile_ReturnsFieldsInStableDisplayOrder()
    {
        var provider = BuildProfileProvider(CountryCode.GB);
        var displayOrders = provider
            .GetProfile(CountryCode.GB)
            .Fields
            .Select(field => field.DisplayOrder)
            .ToArray();

        Assert.Equal(displayOrders.OrderBy(order => order), displayOrders);
        Assert.All(displayOrders, order => Assert.True(order > 0));
    }

    [Fact]
    public void AddressProfile_CopiesFieldCollection()
    {
        var fields = new[]
        {
            new AddressFieldProfile(
                AddressField.AddressLine1,
                "Address line 1",
                true,
                10)
        };

        var profile = new AddressProfile(CountryCode.GB, fields);

        fields[0] = new AddressFieldProfile(
            AddressField.Country,
            "Country",
            true,
            20);

        Assert.Equal(AddressField.AddressLine1, profile.Fields.Single().Field);
        Assert.False(profile.Fields is AddressFieldProfile[]);
    }

    [Fact]
    public void AddAddressing_RegistersAddressProfileProvider()
    {
        var provider = BuildProfileProvider(CountryCode.GB);

        Assert.Equal(CountryCode.GB, provider.GetProfile(CountryCode.GB).CountryCode);
    }

    [Fact]
    public void AddSpainAddressing_RegistersSpanishProfile()
    {
        var services = new ServiceCollection();

        services.AddAddressing();
        services.AddSpainAddressing();

        using var serviceProvider = services.BuildServiceProvider();
        var provider = serviceProvider.GetRequiredService<IAddressProfileProvider>();

        var profile = provider.GetProfile(CountryCode.ES);

        Assert.Equal(AddressProfileSource.CountrySpecific, profile.Source);
        Assert.Equal(
            "Código postal",
            profile.Fields.Single(field => field.Field == AddressField.PostalCode).Label);
    }

    private static IAddressProfileProvider BuildProfileProvider(params CountryCode[] countries)
    {
        var services = new ServiceCollection();
        services.AddAddressing(countries);

        using var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IAddressProfileProvider>();
    }

    private static void AssertRequired(AddressProfile profile, AddressField field)
    {
        Assert.Contains(
            profile.Fields,
            fieldProfile => fieldProfile.Field == field && fieldProfile.IsRequired);
    }
}
