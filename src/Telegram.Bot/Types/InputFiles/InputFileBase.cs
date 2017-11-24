using System.IO;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    public abstract class InputFileBase
    {
        public string FileId { get; protected set; }

        public string Url { get; protected set; }

        public string FileName { get; protected set; }

        public Stream Content { get; protected set; }

        /// <summary>
        /// Type of file to send
        /// </summary>
        public FileType FileType
        {
            get
            {
                if (Content != null) return FileType.Stream;
                if (FileId != null) return FileType.Id;
                if (Url != null) return FileType.Url;
                return FileType.Unknown;
            }
        }
    }
}
