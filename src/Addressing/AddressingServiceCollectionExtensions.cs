using System;
using ISOCodex.Addressing.Validation;
using ISOCodex.Addressing.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing
{
    public static class AddressingServiceCollectionExtensions
    {
        public static IServiceCollection AddAddressing(
            this IServiceCollection services,
            params CountryCode[] countries)
        {
            services.AddSingleton<IAddressValidatorFactory>(_ =>
            {
                var factory = new AddressValidatorFactory();

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
                            throw new ArgumentException(
                                $"No validator available for country code '{country.Code}'.");
                    }
                }

                return factory;
            });

            return services;
        }
    }
}
