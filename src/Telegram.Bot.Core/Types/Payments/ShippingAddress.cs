namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object represents a shipping address.
    /// </summary>
    public class ShippingAddress
    {
        /// <summary>
        /// ISO 3166-1 alpha-2 country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// State, if applicable
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// First line for the address
        /// </summary>
        public string StreetLine1 { get; set; }

        /// <summary>
        /// Second line for the address
        /// </summary>
        public string StreetLine2 { get; set; }

        /// <summary>
        /// Address post code
        /// </summary>
        public string PostCode { get; set; }
    }
}
