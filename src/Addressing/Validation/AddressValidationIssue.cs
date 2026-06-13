using System;

namespace ISOCodex.Addressing.Validation
{
    public sealed class AddressValidationIssue
    {
        public AddressValidationIssue(
            string code,
            string message,
            string? propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code cannot be null or empty.", nameof(code));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            }

            Code = code;
            Message = message;
            PropertyName = propertyName;
        }

        public string Code { get; }

        public string Message { get; }

        public string? PropertyName { get; }
    }
}
