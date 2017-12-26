using System;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.InputFiles
{
    /// <summary>
    /// ToDo
    /// </summary>
    public class InputTelegramFile : InputFileStream
    {
        /// <summary>
        /// Id of a file that exists on Telegram servers
        /// </summary>
        public string FileId { get; protected set; }

        /// <inheritdoc cref="IInputFile.FileType"/>
        public override FileType FileType
        {
            get
            {
                if (Content != null) return FileType.Stream;
                if (FileId != null) return FileType.Id;
                throw new InvalidOperationException("Not a valid InputFile");
            }
        }
    }
}
