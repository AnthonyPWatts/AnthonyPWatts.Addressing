using ISOCodex.Addressing.Utilities;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ISOCodex.Addressing.Spain
{
    public static class SpainServiceCollectionExtensions
    {
        public static IServiceCollection AddSpainAddressing(this IServiceCollection services)
        {
            services.TryAddSingleton<IAddressValidatorFactory, AddressValidatorFactory>();

            services.AddSingleton<IStartupAction>(sp =>
                new StartupAction(() =>
                {
                    var factory = sp.GetRequiredService<IAddressValidatorFactory>();
                    factory.RegisterValidator(CountryCode.Parse("ES"), new SpanishAddressValidator());
                }));

            return services;
        }
    }
}
