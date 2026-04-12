using ISOCodex.Addressing;
using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Utilities;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = BuildAddressingServiceProvider();

var addressValidatorFactory =
    serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var spanishAddress = new Address(
    "123 Main St",
    null,
    "Madrid",
    null,
    new PostalCode("28001", CountryCode.Parse("ES")),
    CountryCode.Parse("ES"));

addressValidatorFactory.GetValidator(spanishAddress.CountryCode).Validate(spanishAddress);
Console.WriteLine("Spanish address is valid!");

var ukAddress = new Address(
    "10 Downing St",
    null,
    "London",
    null,
    new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
    CountryCode.Parse("GB"));

addressValidatorFactory.GetValidator(ukAddress.CountryCode).Validate(ukAddress);
Console.WriteLine("UK address is valid!");

return 0;

static IServiceProvider BuildAddressingServiceProvider()
{
    var services = new ServiceCollection();

    services.AddAddressing(CountryCode.Parse("GB"), CountryCode.Parse("US"));
    services.AddSpainAddressing();

    var serviceProvider = services.BuildServiceProvider();

    foreach (var action in serviceProvider.GetServices<IStartupAction>())
    {
        action.Execute();
    }

    return serviceProvider;
}
