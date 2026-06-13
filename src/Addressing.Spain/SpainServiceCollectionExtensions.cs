using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Spain
{
    public static class SpainServiceCollectionExtensions
    {
        public static IServiceCollection AddSpainAddressing(this IServiceCollection services)
        {
            services.AddAddressValidator(
                CountryCode.ES,
                () => new SpanishAddressValidator());

            services.AddAddressFormatter(
                CountryCode.ES,
                () => new SpanishAddressFormatter());

            return services;
        }
    }
}
