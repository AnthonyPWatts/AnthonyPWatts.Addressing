using ISOCodex.Addressing.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ISOCodex.Addressing.Spain
{
    public static class SpainServiceCollectionExtensions
    {
        private static readonly AddressFieldOption[] SpanishAdministrativeAreas =
        {
            Option("Álava", "Álava"),
            Option("Albacete", "Albacete"),
            Option("Alicante", "Alicante"),
            Option("Almería", "Almería"),
            Option("Ávila", "Ávila"),
            Option("Badajoz", "Badajoz"),
            Option("Balearic Islands", "Balearic Islands"),
            Option("Barcelona", "Barcelona"),
            Option("Burgos", "Burgos"),
            Option("Cáceres", "Cáceres"),
            Option("Cádiz", "Cádiz"),
            Option("Castellón", "Castellón"),
            Option("Ciudad Real", "Ciudad Real"),
            Option("Córdoba", "Córdoba"),
            Option("A Coruña", "A Coruña"),
            Option("Cuenca", "Cuenca"),
            Option("Girona", "Girona"),
            Option("Granada", "Granada"),
            Option("Guadalajara", "Guadalajara"),
            Option("Guipúzcoa", "Guipúzcoa"),
            Option("Huelva", "Huelva"),
            Option("Huesca", "Huesca"),
            Option("Jaén", "Jaén"),
            Option("León", "León"),
            Option("Lleida", "Lleida"),
            Option("La Rioja", "La Rioja"),
            Option("Lugo", "Lugo"),
            Option("Madrid", "Madrid"),
            Option("Málaga", "Málaga"),
            Option("Murcia", "Murcia"),
            Option("Navarra", "Navarra"),
            Option("Ourense", "Ourense"),
            Option("Asturias", "Asturias"),
            Option("Palencia", "Palencia"),
            Option("Las Palmas", "Las Palmas"),
            Option("Pontevedra", "Pontevedra"),
            Option("Salamanca", "Salamanca"),
            Option("Santa Cruz de Tenerife", "Santa Cruz de Tenerife"),
            Option("Cantabria", "Cantabria"),
            Option("Segovia", "Segovia"),
            Option("Sevilla", "Sevilla"),
            Option("Soria", "Soria"),
            Option("Tarragona", "Tarragona"),
            Option("Teruel", "Teruel"),
            Option("Toledo", "Toledo"),
            Option("Valencia", "Valencia"),
            Option("Valladolid", "Valladolid"),
            Option("Vizcaya", "Vizcaya"),
            Option("Zamora", "Zamora"),
            Option("Zaragoza", "Zaragoza"),
            Option("Ceuta", "Ceuta"),
            Option("Melilla", "Melilla")
        };

        public static IServiceCollection AddSpainAddressing(this IServiceCollection services)
        {
            services.AddAddressValidator(
                CountryCode.ES,
                () => new SpanishAddressValidator());

            services.AddAddressFormatter(
                CountryCode.ES,
                () => new SpanishAddressFormatter());

            services.AddAddressProfile(
                CountryCode.ES,
                CreateSpanishAddressProfile);

            return services;
        }

        private static AddressProfile CreateSpanishAddressProfile()
        {
            return new AddressProfile(
                CountryCode.ES,
                new[]
                {
                    Field(AddressField.AddressLine1, "Calle y número", true, 10, "Calle Mayor 10"),
                    Field(AddressField.AddressLine2, "Piso, puerta, escalera", false, 20, "3º C"),
                    Field(AddressField.Locality, "Localidad", true, 30, "Madrid"),
                    Field(
                        AddressField.AdministrativeArea,
                        "Provincia",
                        true,
                        40,
                        "Madrid",
                        AddressFieldInputKind.Select,
                        SpanishAdministrativeAreas),
                    Field(AddressField.PostalCode, "Código postal", true, 50, "28013"),
                    Field(AddressField.Country, "País", true, 60, "España")
                },
                examplePostalCode: "28013",
                exampleFormattedAddress: "Calle Mayor 10\n28013 Madrid\nMadrid\nEspaña");
        }

        private static AddressFieldProfile Field(
            AddressField field,
            string label,
            bool isRequired,
            int displayOrder,
            string? placeholder = null)
        {
            return new AddressFieldProfile(
                field,
                label,
                isRequired,
                displayOrder,
                placeholder);
        }

        private static AddressFieldProfile Field(
            AddressField field,
            string label,
            bool isRequired,
            int displayOrder,
            string? placeholder,
            AddressFieldInputKind inputKind,
            AddressFieldOption[] options)
        {
            return new AddressFieldProfile(
                field,
                label,
                isRequired,
                displayOrder,
                placeholder,
                inputKind: inputKind,
                options: options);
        }

        private static AddressFieldOption Option(string value, string label)
        {
            return new AddressFieldOption(value, label);
        }
    }
}
