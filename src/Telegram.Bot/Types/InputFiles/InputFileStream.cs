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
    public class InputFileStream : IInputFile
    {
        /// <inheritdoc />
        public FileType FileType { get; }

        /// <summary>
        /// File content to upload
        /// </summary>
        public Stream Content { get; }

        /// <summary>
        /// Name of a file to upload using multipart/form-data
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected InputFileStream()
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileType"></param>
        protected InputFileStream(FileType fileType) => FileType = fileType;

        /// <summary>
        /// Constructs an <see cref="InputFileStream"/> from a <see cref="Stream"/> and a file name
        /// </summary>
        /// <param name="content"><see cref="Stream"/> containing the file</param>
        /// <param name="fileName">Name of the file</param>
        public InputFileStream(Stream content, string fileName = default)
        {
            Content = content;
            FileName = fileName;
            FileType = FileType.Stream;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="stream"></param>
        public static implicit operator InputFileStream(Stream stream) =>
            stream is null ? default : new InputFileStream(stream);
    }
}
