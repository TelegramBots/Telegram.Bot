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
    public class InputTelegramFile : InputFileStream
    {
        /// <summary>
        /// Id of a file that exists on Telegram servers
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FileId { get; private protected set; }

        /// <summary>
        ///
        /// </summary>
        protected InputTelegramFile()
        { }

        /// <summary>
        /// ToDo
        /// </summary>
        protected InputTelegramFile(FileType fileType) : base(fileType)
        { }

        /// <summary>
        /// Constructs an <see cref="InputTelegramFile"/> from a <see cref="Stream"/> and a file name
        /// </summary>
        /// <param name="content"><see cref="Stream"/> containing the file</param>
        /// <param name="fileName">Name of the file</param>
        public InputTelegramFile(Stream content, string fileName = default)
            : base(content, fileName)
        { }

        /// <summary>
        /// Constructs an <see cref="InputTelegramFile"/> from a file id
        /// </summary>
        /// <param name="fileId">File id on Telegram's servers</param>
        public InputTelegramFile(string fileId) : base(FileType.Id) =>
            FileId = fileId;

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="stream"></param>
        public static implicit operator InputTelegramFile(Stream stream) =>
            stream is null ? default : new InputTelegramFile(stream);

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="fileId"></param>
        public static implicit operator InputTelegramFile(string fileId) =>
            fileId is null ? default : new InputTelegramFile(fileId);
    }
}
