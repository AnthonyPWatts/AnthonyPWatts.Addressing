using Microsoft.Extensions.DependencyInjection;
using Addressing;
using Addressing.Validation;
using Addressing.Spain;
using Addressing.Utilities;

var serviceProvider = BuildAddressingServiceProvider();
var addressValidatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

// Create a Spanish address
var address = new Address(
    "123 Main St",
    null,
    "Madrid",
    null,
    new PostalCode("28001", CountryCode.Parse("ES")),
    CountryCode.Parse("ES"));

// Validate the Spanish address
var countrySpecificAddressValidator = addressValidatorFactory.GetValidator(address.CountryCode);
countrySpecificAddressValidator.Validate(address);
Console.WriteLine("Spanish address is valid!");

// Now make it a UK Address
address = new Address(
    "10 Downing St",
    null,
    "London",
    null,
    new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
    CountryCode.Parse("GB"));

// Validate the UK address
countrySpecificAddressValidator = addressValidatorFactory.GetValidator(address.CountryCode);
countrySpecificAddressValidator.Validate(address);
Console.WriteLine("UK address is valid!");

return 0;

static IServiceProvider BuildAddressingServiceProvider()
{
    var services = new ServiceCollection();
    // Pull in any validators needed from Core
    services.AddAddressing(new[] { CountryCode.Parse("GB"), CountryCode.Parse("US") });

    // Then add any additional validators needed
    services.AddSpainAddressing();

    // Execute all startup actions to finalise configuration.
    // This is necessary because we register non-core validators in a deferred manner, because .NET Standard.
    var serviceProvider = services.BuildServiceProvider();
    var startupActions = serviceProvider.GetServices<IStartupAction>();
    foreach (var action in startupActions)
    {
        action.Execute();
    }

    return serviceProvider;
}