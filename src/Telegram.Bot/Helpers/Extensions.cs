using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Helpers
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        [Obsolete] private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        [Obsolete("This method will be removed in v15 " +
                  "due to v14 using UnixDateTimeConverter from Newtonsoft.Json")]
        public static DateTime FromUnixTime(this long unixTime)
            => UnixEpoch.AddSeconds(unixTime);

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="OverflowException"></exception>
        [Obsolete("This method will be removed in v15 " +
                  "due to v14 using UnixDateTimeConverter from Newtonsoft.Json")]
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
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Disposition", contentDisposision}
                }
            };

            multipartContent.Add(mediaPartContent, name, fileName);
        }

        internal static void AddContentIfInputFileStream(
            this MultipartFormDataContent multipartContent,
            params IInputMedia[] inputMedia
        )
        {
            foreach (var input in inputMedia)
            {
                if (input.Media.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(input.Media.Content, input.Media.FileName);
                }

                var mediaThumb = (input as IInputMediaThumb)?.Thumb;
                if (mediaThumb?.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(mediaThumb.Content, mediaThumb.FileName);
                }
            }
        }
    }
}
