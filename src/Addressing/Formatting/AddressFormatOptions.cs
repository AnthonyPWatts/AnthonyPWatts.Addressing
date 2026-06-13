namespace ISOCodex.Addressing.Formatting
{
    public sealed class AddressFormatOptions
    {
        public AddressFormatStyle Style { get; set; } = AddressFormatStyle.MultiLine;

        public bool IncludeCountry { get; set; } = true;

        public string SingleLineSeparator { get; set; } = ", ";
    }
}
