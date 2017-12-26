using System;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputFiles
{
    /// <summary>
    /// ToDo
    /// </summary>
    public class InputOnlineFile : InputTelegramFile
    {
        /// <summary>
        /// HTTP URL for Telegram to get a file from the Internet
        /// </summary>
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
    }
}
