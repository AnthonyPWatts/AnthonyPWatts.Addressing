using System;

namespace ISOCodex.Addressing
{
    public readonly struct PostalCode : IEquatable<PostalCode>
    {
        public string Code { get; }

        public PostalCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Postal code cannot be null or empty.", nameof(code));
            }

            Code = code;
        }

        public override string ToString() => Code;

        public override bool Equals(object? obj) => obj is PostalCode other && Equals(other);

        public bool Equals(PostalCode other) => Code == other.Code;

        public override int GetHashCode() => Code is null ? 0 : Code.GetHashCode();

        public static bool operator ==(PostalCode left, PostalCode right) => left.Equals(right);

        public static bool operator !=(PostalCode left, PostalCode right) => !(left == right);
    }
}
