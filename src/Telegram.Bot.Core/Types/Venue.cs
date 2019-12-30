namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a venue.
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Venue location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Title of the result
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue
        /// </summary>
        public string FoursquareId { get; set; }

        /// <summary>
        /// Optional. Foursquare type of the venue. (For example, "arts_entertainment/default",
        /// "arts_entertainment/aquarium" or "food/icecream".)
        /// </summary>
        public string FoursquareType { get; set; }
    }
}
