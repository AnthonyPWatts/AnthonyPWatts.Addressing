using System;

namespace Addressing
{
    public readonly struct PostalCode : IEquatable<PostalCode>
    {
        public string Code { get; }
        public CountryCode Country { get; }

        public PostalCode(string code, CountryCode country)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Postal code cannot be null or empty.", nameof(code));

            Code = code;
            Country = country;
        }

        public override string ToString() => Code;

        public override bool Equals(object obj) => obj is PostalCode other && Equals(other);

        public bool Equals(PostalCode other) => Code == other.Code && Country == other.Country;

        public override int GetHashCode() => HashCode.Combine(Code, Country);

        public static bool operator ==(PostalCode left, PostalCode right) => left.Equals(right);
        public static bool operator !=(PostalCode left, PostalCode right) => !(left == right);
    }
}
