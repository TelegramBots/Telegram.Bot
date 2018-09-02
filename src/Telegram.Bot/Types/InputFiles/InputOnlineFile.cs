using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputFiles
{
    /// <summary>
    /// ToDo
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [JsonConverter(typeof(InputFileConverter))]
    public class InputOnlineFile : InputTelegramFile
    {
        /// <summary>
        /// HTTP URL for Telegram to get a file from the Internet
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; protected set; }

        /// <inheritdoc cref="IInputFile.FileType"/>
        public override FileType FileType
        {
            get
            {
                if (Content != null) return FileType.Stream;
                if (FileId != null) return FileType.Id;
                if (Url != null) return FileType.Url;
                throw new InvalidOperationException("Not a valid InputFile");
            }
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="content"></param>
        public InputOnlineFile(Stream content)
            : this(content, default)
        {
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        public InputOnlineFile(Stream content, string fileName)
        {
            Content = content;
            FileName = fileName;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="value"></param>
        public InputOnlineFile(string value)
        {
            if (Uri.TryCreate(value, UriKind.Absolute, out Uri _))
            {
                Url = value;
            }
            else
            {
                FileId = value;
            }
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="stream"></param>
        public static implicit operator InputOnlineFile(Stream stream) =>
            stream == null
                ? default
                : new InputOnlineFile(stream);

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator InputOnlineFile(string value) =>
            value == null
                ? default
                : new InputOnlineFile(value);
    }
}
