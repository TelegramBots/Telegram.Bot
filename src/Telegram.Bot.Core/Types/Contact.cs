namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a phone contact.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact's phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Contact's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Optional. Contact's user identifier in Telegram
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Optional. Additional data about the contact in the form of a vCard
        /// </summary>
        public string Vcard { get; set; }
    }
}
