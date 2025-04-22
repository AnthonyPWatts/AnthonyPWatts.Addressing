using Addressing.Utilities;
using Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Addressing.Spain
{
    public static class SpainServiceCollectionExtensions
    {
        /// <summary>
        /// Adds support for Spanish addressing to the application.
        /// This method ensures that the Spanish address validator is registered with the existing
        /// IAddressValidatorFactory. Due to limitations in .NET Standard 2.1 (e.g., lack of PostConfigure),
        /// we use a custom mechanism to defer the registration until the service provider is built.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddSpainAddressing(this IServiceCollection services)
        {
            // Ensure the factory is registered if it isn't already.
            // TryAddSingleton ensures that we don't overwrite an existing registration for IAddressValidatorFactory.
            services.TryAddSingleton<IAddressValidatorFactory, AddressValidatorFactory>();

            // Here comes the "ugly" part:
            // We cannot directly modify the factory at this point because the service provider
            // (which resolves dependencies) hasn't been built yet. Instead, we register a custom
            // "startup action" that will execute after the service provider is built.
            services.AddSingleton<IStartupAction>(sp =>
            {
                // The startup action will resolve the factory and register the Spanish validator.
                return new StartupAction(() =>
                {
                    // Resolve the factory from the service provider.
                    var factory = sp.GetRequiredService<IAddressValidatorFactory>();

                    // Register the Spanish validator with the factory.
                    // This ensures that the validator is available for the "ES" country code.
                    factory.RegisterValidator(CountryCode.Parse("ES"), new SpanishAddressValidator());
                });
            });

            // Return the updated IServiceCollection for chaining.
            return services;
        }
    }    
}
