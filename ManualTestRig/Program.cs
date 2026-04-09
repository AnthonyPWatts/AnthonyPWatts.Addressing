using Microsoft.Extensions.DependencyInjection;
using Addressing;
using Addressing.Validation;
using Addressing.Spain;
using Addressing.Utilities;

// This is a test rig to validate the Addressing library's functionality.
// It demonstrates how to use the library to validate addresses in different countries.
// Check out BuildAddressingServiceProvider, which is where everything gets configured.
var serviceProvider = BuildAddressingServiceProvider();

// Get the address validator factory from the service provider
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

// Fail to validate the now UK address with the Spanish validator
try
{
    countrySpecificAddressValidator.Validate(address);
    Console.WriteLine("Unexpectedly, address is valid!");
}
catch (Exception ex)
{
    Console.WriteLine($"Validation failed, as expected, with message: {ex.Message}");
}

// Validate the UK address with a UK validator
countrySpecificAddressValidator = addressValidatorFactory.GetValidator(address.CountryCode);
countrySpecificAddressValidator.Validate(address);
Console.WriteLine("UK address is valid!");

// Now make it an Australian address, which we don't have any validators defined for
address = new Address(
    "1 Infinite Loop",
    null,
    "Sydney",
    null,
    new PostalCode("2000", CountryCode.Parse("AU")),
    CountryCode.Parse("AU"));

// Prove it fails with the existing (Spanish) validator
try
{
    countrySpecificAddressValidator.Validate(address);
    Console.WriteLine("Unexpectedly, address is valid!");
}
catch (Exception ex)
{
    Console.WriteLine($"Validation failed, as expected, with message: {ex.Message}");
}

// PRove it failes to get a validator for Australia
try
{
    countrySpecificAddressValidator = addressValidatorFactory.GetValidator(address.CountryCode);
    Console.WriteLine("Unexpectedly, address got itself a validator!");
}
catch (Exception ex)
{
    Console.WriteLine($"Validator fetch failed, as expected, with message: {ex.Message}");
}



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