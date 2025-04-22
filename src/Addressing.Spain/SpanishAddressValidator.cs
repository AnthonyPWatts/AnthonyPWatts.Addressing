using Addressing.Validation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Addressing.Spain
{
    public class SpanishAddressValidator : IAddressValidator
    {
        private static readonly Regex PostalCodeRegex = new Regex(@"^\d{5}$", RegexOptions.Compiled);

        // List of valid Spanish provinces
        private static readonly HashSet<string> ValidProvinces = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Álava", "Albacete", "Alicante", "Almería", "Ávila", "Badajoz", "Balearic Islands", "Barcelona", "Burgos",
                "Cáceres", "Cádiz", "Castellón", "Ciudad Real", "Córdoba", "A Coruña", "Cuenca", "Girona", "Granada",
                "Guadalajara", "Guipúzcoa", "Huelva", "Huesca", "Jaén", "León", "Lleida", "La Rioja", "Lugo", "Madrid",
                "Málaga", "Murcia", "Navarra", "Ourense", "Asturias", "Palencia", "Las Palmas", "Pontevedra", "Salamanca",
                "Santa Cruz de Tenerife", "Cantabria", "Segovia", "Sevilla", "Soria", "Tarragona", "Teruel", "Toledo",
                "Valencia", "Valladolid", "Vizcaya", "Zamora", "Zaragoza", "Ceuta", "Melilla"
            };

        // Mapping of postal code ranges to provinces
        private static readonly Dictionary<string, string> PostalCodeToProvince = new Dictionary<string, string>
            {
                { "01", "Álava" }, { "02", "Albacete" }, { "03", "Alicante" }, { "04", "Almería" }, { "05", "Ávila" },
                { "06", "Badajoz" }, { "07", "Balearic Islands" }, { "08", "Barcelona" }, { "09", "Burgos" },
                { "10", "Cáceres" }, { "11", "Cádiz" }, { "12", "Castellón" }, { "13", "Ciudad Real" }, { "14", "Córdoba" },
                { "15", "A Coruña" }, { "16", "Cuenca" }, { "17", "Girona" }, { "18", "Granada" }, { "19", "Guadalajara" },
                { "20", "Guipúzcoa" }, { "21", "Huelva" }, { "22", "Huesca" }, { "23", "Jaén" }, { "24", "León" },
                { "25", "Lleida" }, { "26", "La Rioja" }, { "27", "Lugo" }, { "28", "Madrid" }, { "29", "Málaga" },
                { "30", "Murcia" }, { "31", "Navarra" }, { "32", "Ourense" }, { "33", "Asturias" }, { "34", "Palencia" },
                { "35", "Las Palmas" }, { "36", "Pontevedra" }, { "37", "Salamanca" }, { "38", "Santa Cruz de Tenerife" },
                { "39", "Cantabria" }, { "40", "Segovia" }, { "41", "Sevilla" }, { "42", "Soria" }, { "43", "Tarragona" },
                { "44", "Teruel" }, { "45", "Toledo" }, { "46", "Valencia" }, { "47", "Valladolid" }, { "48", "Vizcaya" },
                { "49", "Zamora" }, { "50", "Zaragoza" }, { "51", "Ceuta" }, { "52", "Melilla" }
            };

        public void Validate(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");

            if (string.IsNullOrWhiteSpace(address.Line1))
                throw new ArgumentException("Line1 cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(address.City))
                throw new ArgumentException("City cannot be null or empty.");

            if (address.CountryCode.Code != "ES")
                throw new ArgumentException("CountryCode must be 'ES' for Spanish addresses.");

            if (!PostalCodeRegex.IsMatch(address.PostalCode.Code))
                throw new ArgumentException("PostalCode must be a 5-digit number for Spanish addresses.");

            // Validate province if provided
            if (!string.IsNullOrWhiteSpace(address.StateOrProvince))
            {
                if (!ValidProvinces.Contains(address.StateOrProvince))
                    throw new ArgumentException($"StateOrProvince '{address.StateOrProvince}' is not a valid Spanish province.");
            }

            // Validate postal code range
            var postalCodePrefix = address.PostalCode.Code.Substring(0, 2); // First two digits
            if (PostalCodeToProvince.TryGetValue(postalCodePrefix, out var expectedProvince))
            {
                if (!string.IsNullOrWhiteSpace(address.StateOrProvince) &&
                    !string.Equals(address.StateOrProvince, expectedProvince, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException($"PostalCode '{address.PostalCode.Code}' does not match the provided StateOrProvince '{address.StateOrProvince}'.");
                }
            }
            else
            {
                throw new ArgumentException($"PostalCode '{address.PostalCode.Code}' is not valid for any known Spanish province.");
            }
        }
    }
}
