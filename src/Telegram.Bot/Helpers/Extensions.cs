using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Helpers
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(this long unixTime)
            => UnixEpoch.AddSeconds(unixTime);

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static long ToUnixTime(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return 0;

            var utcDateTime = dateTime.ToUniversalTime();

            var delta = (utcDateTime - UnixEpoch).TotalSeconds;

            if (delta < 0)
                throw new ArgumentOutOfRangeException(nameof(dateTime), "Unix epoch starts January 1st, 1970");

            return Convert.ToInt64(delta);
        }

        internal static string EncodeUtf8(this string value) =>
            string.Join(string.Empty, Encoding.UTF8.GetBytes(value).Select(Convert.ToChar));

        internal static void AddStreamContent(
            this MultipartFormDataContent multipartContent,
            Stream content,
            string name,
            string fileName = default)
        {
            fileName = fileName ?? name;
            string contentDisposision = $@"form-data; name=""{name}""; filename=""{fileName}""".EncodeUtf8();

            HttpContent mediaPartContent = new StreamContent(content)
            {
                Headers =
                {
                    { "Content-Type", "application/octet-stream" },
                    { "Content-Disposition", contentDisposision }
                }
            };

            multipartContent.Add(mediaPartContent, name, fileName);
        }

        [Obsolete]
        internal static string ToSnakeCased(this string value)
            => new SnakeCaseNamingStrategy().GetPropertyName(value, false);
    }
}
