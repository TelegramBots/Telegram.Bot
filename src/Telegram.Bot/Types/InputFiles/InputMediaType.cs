using System;
using System.IO;
using Newtonsoft.Json;
using Telegram.Bot.Converters;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Media to send in request that could be a file_id, HTTP url, or a file
    /// </summary>
    [JsonConverter(typeof(InputMediaTypeConverter))]
    public class InputMediaType : InputFileBase
    {
        /// <summary>
        /// Initializes media with either a file_id or a HTTP url
        /// </summary>
        /// <param name="value">file_id to send a file that exists on the Telegram servers or an HTTP URL for Telegram to get a file from the Internet</param>
        public InputMediaType(string value)
        {
            bool isUrl = Uri.TryCreate(value, UriKind.Absolute, out Uri _);
            if (isUrl)
            {
                Url = value;
            }
            else
            {
                FileId = value;
            }
        }

        /// <summary>
        /// Initializes media with a file to send
        /// </summary>
        /// <param name="fileName">Name of the file to send</param>
        /// <param name="content">File content to upload</param>
        public InputMediaType(string fileName, Stream content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
