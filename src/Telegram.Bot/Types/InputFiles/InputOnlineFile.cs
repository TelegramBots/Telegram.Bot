using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputFiles
{
    /// <summary>
    /// Used for sending files to Telegram
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [JsonConverter(typeof(InputFileConverter))]
    public class InputOnlineFile : InputTelegramFile
    {
        /// <summary>
        /// HTTP URL for Telegram to get a file from the Internet
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; }

        /// <summary>
        /// Constructs an <see cref="InputOnlineFile"/> from a <see cref="Stream"/> and a file name
        /// </summary>
        /// <param name="content"><see cref="Stream"/> containing the file</param>
        /// <param name="fileName">Name of the file</param>
        public InputOnlineFile(Stream content, string fileName = default)
            : base(content, fileName)
        { }

        /// <summary>
        /// Constructs an <see cref="InputOnlineFile"/> from a string containing a uri or file id
        /// </summary>
        /// <param name="value"><see cref="string"/> containing a url or file id</param>
        public InputOnlineFile(string value) : base(Check(value, out var isUrl))
        {
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
        /// Constructs an <see cref="InputOnlineFile"/> from a <see cref="Uri"/>
        /// </summary>
        /// <param name="url"><see cref="Uri"/> pointing to a file</param>
        public InputOnlineFile(Uri url) : base(FileType.Url) => Url = url.AbsoluteUri;

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="stream"></param>
        public static implicit operator InputOnlineFile(Stream stream) =>
            stream is null ? default : new InputOnlineFile(stream);

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator InputOnlineFile(string value) =>
            value is null ? default : new InputOnlineFile(value);

        private static FileType Check(string value, out bool isUrl)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out _))
            {
                isUrl = true;
                return FileType.Url;
            }
            isUrl = false;
            return FileType.Id;
        }
    }
}
