using Addressing.Validation;
using Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Addressing
{
    public static class AddressingServiceCollectionExtensions
    {
        /// <summary>
        /// Adds core addressing services to the application.
        /// Allows optional registration of country-specific validators.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure.</param>
        /// <param name="countries">An array of CountryCode objects specifying which validators to register.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddAddressing(this IServiceCollection services, params CountryCode[] countries)
        {
            // Register the DefaultAddressValidatorFactory
            services.AddSingleton<IAddressValidatorFactory>(provider =>
            {
                var factory = new AddressValidatorFactory();

                // Register validators for the specified countries
                foreach (var country in countries)
                {
                    switch (country.Code)
                    {
                        case "US":
                            factory.RegisterValidator(country, new USAddressValidator());
                            break;
                        case "GB":
                            factory.RegisterValidator(country, new UKAddressValidator());
                            break;
                        case "CA":
                            factory.RegisterValidator(country, new CanadianAddressValidator());
                            break;
                        default:
                            throw new ArgumentException($"No validator available for country code '{country.Code}'.");
                    }
                }

                return factory;
            });

            return services;
        }
    }
}
