using System;
using ISOCodex.Addressing.Formatting;
using ISOCodex.Addressing.Formatting.Formatters;
using ISOCodex.Addressing.Profiles;
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
            services.TryAddAddressFormatter();
            services.TryAddAddressProfileProvider();

            foreach (var country in countries)
            {
                services.AddBuiltInAddressValidator(country);
                services.AddBuiltInAddressFormatter(country);
                services.AddBuiltInAddressProfile(country);
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

        public static IServiceCollection AddAddressFormatter(
            this IServiceCollection services,
            CountryCode country,
            Func<ICountryAddressFormatter> formatterFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (formatterFactory == null)
            {
                throw new ArgumentNullException(nameof(formatterFactory));
            }

            services.TryAddAddressFormatter();
            services.AddSingleton(new AddressFormatterRegistration(country, formatterFactory));

            return services;
        }

        public static IServiceCollection AddAddressProfile(
            this IServiceCollection services,
            CountryCode country,
            Func<AddressProfile> profileFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (profileFactory == null)
            {
                throw new ArgumentNullException(nameof(profileFactory));
            }

            services.TryAddAddressProfileProvider();
            services.AddSingleton(new AddressProfileRegistration(country, profileFactory));

            return services;
        }

        public static IServiceCollection AddFallbackAddressValidator(
            this IServiceCollection services,
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
            services.AddSingleton(new AddressValidatorFallbackRegistration(validatorFactory));

            return services;
        }

        public static IServiceCollection AddFallbackAddressFormatter(
            this IServiceCollection services,
            Func<ICountryAddressFormatter> formatterFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (formatterFactory == null)
            {
                throw new ArgumentNullException(nameof(formatterFactory));
            }

            services.TryAddAddressFormatter();
            services.AddSingleton(new AddressFormatterFallbackRegistration(formatterFactory));

            return services;
        }

        public static IServiceCollection AddFallbackAddressProfile(
            this IServiceCollection services,
            Func<AddressProfile> profileFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (profileFactory == null)
            {
                throw new ArgumentNullException(nameof(profileFactory));
            }

            services.TryAddAddressProfileProvider();
            services.AddSingleton(new AddressProfileFallbackRegistration(profileFactory));

            return services;
        }

        public static IServiceCollection AddGenericAddressingFallbacks(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddFallbackAddressValidator(() => new PermissiveAddressValidator());
            services.AddFallbackAddressFormatter(() => new GenericAddressFormatter());
            services.AddFallbackAddressProfile(() => AddressProfileDefaults.CreateGenericFallbackProfile());

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

        private static IServiceCollection AddBuiltInAddressFormatter(
            this IServiceCollection services,
            CountryCode country)
        {
            switch (country.Code)
            {
                case "US":
                    return services.AddAddressFormatter(country, () => new USAddressFormatter());
                case "GB":
                    return services.AddAddressFormatter(country, () => new GBAddressFormatter());
                case "CA":
                    return services.AddAddressFormatter(country, () => new CAAddressFormatter());
                default:
                    throw new ArgumentException(
                        $"No formatter available for country code '{country.Code}'.");
            }
        }

        private static IServiceCollection AddBuiltInAddressProfile(
            this IServiceCollection services,
            CountryCode country)
        {
            switch (country.Code)
            {
                case "US":
                    return services.AddAddressProfile(
                        country,
                        () => AddressProfileDefaults.CreateUnitedStatesProfile());
                case "GB":
                    return services.AddAddressProfile(
                        country,
                        () => AddressProfileDefaults.CreateGreatBritainProfile());
                case "CA":
                    return services.AddAddressProfile(
                        country,
                        () => AddressProfileDefaults.CreateCanadaProfile());
                default:
                    throw new ArgumentException(
                        $"No profile available for country code '{country.Code}'.");
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

                foreach (var registration in sp.GetServices<AddressValidatorFallbackRegistration>())
                {
                    factory.RegisterFallbackValidator(registration.CreateValidator());
                }

                return factory;
            });
        }

        private static void TryAddAddressFormatter(this IServiceCollection services)
        {
            services.TryAddSingleton<IAddressFormatter>(sp =>
            {
                var formatter = new AddressFormatter();

                foreach (var registration in sp.GetServices<AddressFormatterRegistration>())
                {
                    formatter.RegisterFormatter(
                        registration.Country,
                        registration.CreateFormatter());
                }

                foreach (var registration in sp.GetServices<AddressFormatterFallbackRegistration>())
                {
                    formatter.RegisterFallbackFormatter(registration.CreateFormatter());
                }

                return formatter;
            });
        }

        private static void TryAddAddressProfileProvider(this IServiceCollection services)
        {
            services.TryAddSingleton<IAddressProfileProvider>(sp =>
            {
                var provider = new DefaultAddressProfileProvider();

                foreach (var registration in sp.GetServices<AddressProfileRegistration>())
                {
                    provider.RegisterProfile(
                        registration.Country,
                        registration.CreateProfile());
                }

                foreach (var registration in sp.GetServices<AddressProfileFallbackRegistration>())
                {
                    provider.RegisterFallbackProfile(registration.CreateProfile());
                }

                return provider;
            });
        }
    }
}
