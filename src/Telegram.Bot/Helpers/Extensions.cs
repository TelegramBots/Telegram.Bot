using System;

namespace Telegram.Bot.Helpers
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        private static readonly DateTime UnixStart = new DateTime(1970, 1, 1);

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(this long dateTime) 
            => UnixStart.AddSeconds(dateTime).ToLocalTime();

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static long ToUnixTime(this DateTime dateTime)
        {
            var utcDateTime = dateTime.ToUniversalTime();

            if (utcDateTime == DateTime.MinValue)
                return 0;

            var delta = dateTime - UnixStart;

            if (delta.TotalSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(dateTime), "Unix epoc starts January 1st, 1970");

            return Convert.ToInt64(delta.TotalSeconds);
        }
    }
}
