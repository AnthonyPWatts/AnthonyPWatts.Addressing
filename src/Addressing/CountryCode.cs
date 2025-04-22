using System;
using System.Collections.Generic;
using System.Linq;

namespace Addressing 
{
    public readonly struct CountryCode : IEquatable<CountryCode>
    {
        public string Code { get; }

        private CountryCode(string code)
        {
            Code = code.ToUpperInvariant();
        }

        public static CountryCode Parse(string input)
        {
            if (TryParse(input, out var countryCode))
                return countryCode;

            throw new ArgumentException($"Invalid country code: '{input}'. Must be an ISO 3166-1 alpha-2 code.");
        }

        public static bool TryParse(string input, out CountryCode countryCode)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var code = input.ToUpperInvariant();
                if (_validCodes.Contains(code))
                {
                    countryCode = new CountryCode(code);
                    return true;
                }
            }

            countryCode = default;
            return false;
        }

        public static bool IsValid(string input)
        {
            return TryParse(input, out _);
        }

        public override string ToString() => Code;

        public override bool Equals(object obj) => obj is CountryCode other && Equals(other);

        public bool Equals(CountryCode other) => Code == other.Code;

        public override int GetHashCode() => Code != null ? Code.GetHashCode() : 0;

        public static bool operator ==(CountryCode left, CountryCode right) => left.Equals(right);
        public static bool operator !=(CountryCode left, CountryCode right) => !(left == right);

        public static IEnumerable<CountryCode> All => _validCodes.Select(code => new CountryCode(code));

        private static readonly HashSet<string> _validCodes = new HashSet<string>
        {
            "AD", "AE", "AF", "AG", "AI", "AL", "AM", "AO", "AQ", "AR", "AS", "AT", "AU", "AW", "AX", "AZ",
            "BA", "BB", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BL", "BM", "BN", "BO", "BQ", "BR", "BS", "BT", "BV", "BW", "BY", "BZ",
            "CA", "CC", "CD", "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN", "CO", "CR", "CU", "CV", "CW", "CX", "CY", "CZ",
            "DE", "DJ", "DK", "DM", "DO", "DZ",
            "EC", "EE", "EG", "EH", "ER", "ES", "ET",
            "FI", "FJ", "FM", "FO", "FR",
            "GA", "GB", "GD", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GP", "GQ", "GR", "GT", "GU", "GW", "GY",
            "HK", "HM", "HN", "HR", "HT", "HU",
            "ID", "IE", "IL", "IM", "IN", "IO", "IQ", "IR", "IS", "IT",
            "JE", "JM", "JO", "JP",
            "KE", "KG", "KH", "KI", "KM", "KN", "KP", "KR", "KW", "KY", "KZ",
            "LA", "LB", "LC", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY",
            "MA", "MC", "MD", "ME", "MF", "MG", "MH", "MK", "ML", "MM", "MN", "MO", "MP", "MQ", "MR", "MS", "MT", "MU", "MV", "MW", "MX", "MY", "MZ",
            "NA", "NC", "NE", "NF", "NG", "NI", "NL", "NO", "NP", "NR", "NU", "NZ",
            "OM",
            "PA", "PE", "PF", "PG", "PH", "PK", "PL", "PM", "PN", "PR", "PT", "PW", "PY",
            "QA",
            "RE", "RO", "RS", "RU", "RW",
            "SA", "SB", "SC", "SD", "SE", "SG", "SH", "SI", "SJ", "SK", "SL", "SM", "SN", "SO", "SR", "SS", "ST", "SV", "SX", "SY", "SZ",
            "TC", "TD", "TF", "TG", "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TR", "TT", "TV", "TZ",
            "UA", "UG", "UM", "US", "UY", "UZ",
            "VA", "VC", "VE", "VG", "VI", "VN", "VU",
            "WF", "WS",
            "YE", "YT",
            "ZA", "ZM", "ZW"
        };
    }
}

