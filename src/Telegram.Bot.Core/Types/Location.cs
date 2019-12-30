namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a point on the map.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Longitude as defined by sender
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Latitude as defined by sender
        /// </summary>
        public float Latitude { get; set; }
    }
}
