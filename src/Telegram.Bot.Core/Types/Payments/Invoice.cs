namespace Telegram.Bot.Types.Payments
{
    /// <summary>
    /// This object contains basic information about an invoice.
    /// </summary>
    /// <seealso href="https://core.telegram.org/bots/api#invoice"/>
    public class Invoice
    {
        /// <summary>
        /// Product name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Unique bot deep-linking parameter that can be used to generate this invoice
        /// </summary>
        public string StartParameter { get; set; }

        /// <summary>
        /// Three-letter ISO 4217 currency code
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Total price in the smallest units of the currency.
        /// </summary>
        public int TotalAmount { get; set; }
    }
}
