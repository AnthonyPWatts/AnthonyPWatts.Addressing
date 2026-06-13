namespace ISOCodex.Addressing.Profiles
{
    public sealed class AddressFieldProfile
    {
        public AddressFieldProfile(
            AddressField field,
            string label,
            bool isRequired,
            int displayOrder,
            string? placeholder = null,
            string? helpText = null,
            AddressFieldInputKind inputKind = AddressFieldInputKind.Text,
            bool isUsed = true)
        {
            Field = field;
            Label = label;
            IsRequired = isRequired;
            IsUsed = isUsed;
            DisplayOrder = displayOrder;
            Placeholder = placeholder;
            HelpText = helpText;
            InputKind = inputKind;
        }

        public AddressField Field { get; }

        public string Label { get; }

        public bool IsRequired { get; }

        public bool IsUsed { get; }

        public int DisplayOrder { get; }

        public string? Placeholder { get; }

        public string? HelpText { get; }

        public AddressFieldInputKind InputKind { get; }
    }
}
