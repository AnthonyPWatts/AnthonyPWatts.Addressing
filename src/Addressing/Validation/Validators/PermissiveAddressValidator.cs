namespace ISOCodex.Addressing.Validation.Validators
{
    public sealed class PermissiveAddressValidator : IAddressValidator
    {
        public AddressValidationResult Validate(Address? address)
        {
            if (address != null)
            {
                return AddressValidationResult.Success;
            }

            return new AddressValidationResult(
                new[]
                {
                    new AddressValidationIssue(
                        "Address.Required",
                        "Address cannot be null.")
                });
        }
    }
}
