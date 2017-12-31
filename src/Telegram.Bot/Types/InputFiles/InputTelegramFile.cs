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
    public class InputTelegramFile : InputFileStream
    {
        /// <summary>
        /// Id of a file that exists on Telegram servers
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FileId { get; protected set; }

        /// <inheritdoc cref="IInputFile.FileType"/>
        public override FileType FileType
        {
            get
            {
                if (Content != null) return FileType.Stream;
                if (FileId != null) return FileType.Id;
                throw new InvalidOperationException("Not a valid Input File");
            }
        }

        /// <summary>
        /// ToDo
        /// </summary>
        protected InputTelegramFile()
        { }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="content"></param>
        public InputTelegramFile(Stream content)
            : this(content, default)
        { }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        public InputTelegramFile(Stream content, string fileName)
        {
            Content = content;
            FileName = fileName;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="fileId"></param>
        public InputTelegramFile(string fileId)
        {
            FileId = fileId;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="stream"></param>
        public static implicit operator InputTelegramFile(Stream stream)
            => stream is default
                ? default
                : new InputTelegramFile(stream);

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="fileId"></param>
        public static implicit operator InputTelegramFile(string fileId)
            => fileId is default
                ? default
                : new InputTelegramFile(fileId);
    }
}
