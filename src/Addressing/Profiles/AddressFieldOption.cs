namespace ISOCodex.Addressing.Profiles
{
    public sealed class AddressFieldOption
    {
        public AddressFieldOption(string value, string label)
        {
            Value = value;
            Label = label;
        }

        public string Value { get; }

        public string Label { get; }
    }
}
