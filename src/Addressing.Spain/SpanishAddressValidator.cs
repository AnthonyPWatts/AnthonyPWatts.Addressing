using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ISOCodex.Addressing.Validation;

namespace ISOCodex.Addressing.Spain
{
    public class SpanishAddressValidator : IAddressValidator
    {
        private static readonly Regex PostalCodeRegex =
            new Regex(@"^\d{5}$", RegexOptions.Compiled);

        private static readonly HashSet<string> ValidProvinces =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Álava","Albacete","Alicante","Almería","Ávila","Badajoz","Balearic Islands",
                "Barcelona","Burgos","Cáceres","Cádiz","Castellón","Ciudad Real","Córdoba",
                "A Coruña","Cuenca","Girona","Granada","Guadalajara","Guipúzcoa","Huelva",
                "Huesca","Jaén","León","Lleida","La Rioja","Lugo","Madrid","Málaga","Murcia",
                "Navarra","Ourense","Asturias","Palencia","Las Palmas","Pontevedra","Salamanca",
                "Santa Cruz de Tenerife","Cantabria","Segovia","Sevilla","Soria","Tarragona",
                "Teruel","Toledo","Valencia","Valladolid","Vizcaya","Zamora","Zaragoza",
                "Ceuta","Melilla"
            };

        private static readonly Dictionary<string, string> PostalCodeToProvince =
            new Dictionary<string, string>
            {
                { "01", "Álava" }, { "02", "Albacete" }, { "03", "Alicante" },
                { "04", "Almería" }, { "05", "Ávila" }, { "06", "Badajoz" },
                { "07", "Balearic Islands" }, { "08", "Barcelona" }, { "09", "Burgos" },
                { "10", "Cáceres" }, { "11", "Cádiz" }, { "12", "Castellón" },
                { "13", "Ciudad Real" }, { "14", "Córdoba" }, { "15", "A Coruña" },
                { "16", "Cuenca" }, { "17", "Girona" }, { "18", "Granada" },
                { "19", "Guadalajara" }, { "20", "Guipúzcoa" }, { "21", "Huelva" },
                { "22", "Huesca" }, { "23", "Jaén" }, { "24", "León" },
                { "25", "Lleida" }, { "26", "La Rioja" }, { "27", "Lugo" },
                { "28", "Madrid" }, { "29", "Málaga" }, { "30", "Murcia" },
                { "31", "Navarra" }, { "32", "Ourense" }, { "33", "Asturias" },
                { "34", "Palencia" }, { "35", "Las Palmas" }, { "36", "Pontevedra" },
                { "37", "Salamanca" }, { "38", "Santa Cruz de Tenerife" },
                { "39", "Cantabria" }, { "40", "Segovia" }, { "41", "Sevilla" },
                { "42", "Soria" }, { "43", "Tarragona" }, { "44", "Teruel" },
                { "45", "Toledo" }, { "46", "Valencia" }, { "47", "Valladolid" },
                { "48", "Vizcaya" }, { "49", "Zamora" }, { "50", "Zaragoza" },
                { "51", "Ceuta" }, { "52", "Melilla" }
            };

        public AddressValidationResult Validate(Address? address)
        {
            var issues = new List<AddressValidationIssue>();

            AddCommonIssues(issues, address);

            if (address == null)
            {
                return new AddressValidationResult(issues);
            }

            if (!PostalCodeRegex.IsMatch(address.PostalCode.Code))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.PostalCode.Invalid",
                    "PostalCode must be a 5-digit number for Spanish addresses.",
                    nameof(Address.PostalCode)));
            }

            if (!string.IsNullOrWhiteSpace(address.StateOrProvince) &&
                !ValidProvinces.Contains(address.StateOrProvince))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.StateOrProvince.Invalid",
                    $"StateOrProvince '{address.StateOrProvince}' is not a valid Spanish province.",
                    nameof(Address.StateOrProvince)));
            }

            if (!PostalCodeRegex.IsMatch(address.PostalCode.Code))
            {
                return new AddressValidationResult(issues);
            }

            var postalCodePrefix = address.PostalCode.Code.Substring(0, 2);

            if (!PostalCodeToProvince.TryGetValue(postalCodePrefix, out var expectedProvince))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.PostalCode.ProvinceUnknown",
                    $"PostalCode '{address.PostalCode.Code}' is not valid for any known Spanish province.",
                    nameof(Address.PostalCode)));
            }

            if (!string.IsNullOrWhiteSpace(address.StateOrProvince) &&
                expectedProvince != null &&
                !string.Equals(address.StateOrProvince, expectedProvince, StringComparison.OrdinalIgnoreCase))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.PostalCode.ProvinceMismatch",
                    $"PostalCode '{address.PostalCode.Code}' does not match the provided StateOrProvince '{address.StateOrProvince}'.",
                    nameof(Address.PostalCode)));
            }

            return new AddressValidationResult(issues);
        }

        private static void AddCommonIssues(
            ICollection<AddressValidationIssue> issues,
            Address? address)
        {
            if (address == null)
            {
                issues.Add(new AddressValidationIssue(
                    "Address.Required",
                    "Address cannot be null."));
                return;
            }

            if (string.IsNullOrWhiteSpace(address.Line1))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.Line1.Required",
                    "Line1 cannot be null or empty.",
                    nameof(Address.Line1)));
            }

            if (string.IsNullOrWhiteSpace(address.City))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.City.Required",
                    "City cannot be null or empty.",
                    nameof(Address.City)));
            }

            if (address.CountryCode != CountryCode.ES)
            {
                issues.Add(new AddressValidationIssue(
                    "Address.CountryCode.Invalid",
                    "CountryCode must be 'ES' for Spanish addresses.",
                    nameof(Address.CountryCode)));
            }
        }
    }
}
