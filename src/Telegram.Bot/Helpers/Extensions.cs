using System;

namespace Telegram.Bot.Helpers
{
    public static class Extensions
    {
        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(this long self)
        {
            var ret = new DateTime(1970, 1, 1);
            return ret.AddSeconds(self);
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long ToUnixTime(this DateTime self)
        {

            if (self == DateTime.MinValue)
            {
                return 0;
            }

            var epoc = new DateTime(1970, 1, 1);
            var delta = self - epoc;

            if (delta.TotalSeconds < 0) throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");

            return (long) delta.TotalSeconds;
        }
    }
}
