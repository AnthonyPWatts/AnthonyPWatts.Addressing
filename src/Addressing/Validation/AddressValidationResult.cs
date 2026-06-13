using System.Collections.Generic;
using System.Linq;

namespace ISOCodex.Addressing.Validation
{
    public sealed class AddressValidationResult
    {
        public AddressValidationResult(IEnumerable<AddressValidationIssue> issues)
        {
            Issues = issues.ToList();
        }

        public static AddressValidationResult Success { get; } =
            new AddressValidationResult(Enumerable.Empty<AddressValidationIssue>());

        public bool IsValid => Issues.Count == 0;

        public IReadOnlyList<AddressValidationIssue> Issues { get; }
    }
}
