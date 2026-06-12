using System;
using ISOCodex.Addressing.Validation;
using ISOCodex.Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ISOCodex.Addressing
{
    public static class AddressingServiceCollectionExtensions
    {
        public static IServiceCollection AddAddressing(
            this IServiceCollection services,
            params CountryCode[] countries)
        {
            services.TryAddAddressValidatorFactory();

            foreach (var country in countries)
            {
                services.AddBuiltInAddressValidator(country);
            }

            return services;
        }

        public static IServiceCollection AddAddressValidator(
            this IServiceCollection services,
            CountryCode country,
            Func<IAddressValidator> validatorFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (validatorFactory == null)
            {
                throw new ArgumentNullException(nameof(validatorFactory));
            }

            services.TryAddAddressValidatorFactory();
            services.AddSingleton(new AddressValidatorRegistration(country, validatorFactory));

            return services;
        }

        private static IServiceCollection AddBuiltInAddressValidator(
            this IServiceCollection services,
            CountryCode country)
        {
            switch (country.Code)
            {
                case "US":
                    return services.AddAddressValidator(country, () => new USAddressValidator());
                case "GB":
                    return services.AddAddressValidator(country, () => new GBAddressValidator());
                case "CA":
                    return services.AddAddressValidator(country, () => new CAAddressValidator());
                default:
                    throw new ArgumentException(
                        $"No validator available for country code '{country.Code}'.");
            }
        }

        private static void TryAddAddressValidatorFactory(this IServiceCollection services)
        {
            services.TryAddSingleton<IAddressValidatorFactory>(sp =>
            {
                var factory = new AddressValidatorFactory();

                foreach (var registration in sp.GetServices<AddressValidatorRegistration>())
                {
                    factory.RegisterValidator(
                        registration.Country,
                        registration.CreateValidator());
                }

                return factory;
            });
        }
    }
}
