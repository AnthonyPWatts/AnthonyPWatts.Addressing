using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Spain
{
    public static class SpainServiceCollectionExtensions
    {
        public static IServiceCollection AddSpainAddressing(this IServiceCollection services)
        {
            return services.AddAddressValidator(
                CountryCode.Parse("ES"),
                () => new SpanishAddressValidator());
        }
    }
}
