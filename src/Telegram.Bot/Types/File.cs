namespace Telegram.Bot.Types;

/// <summary>This object represents a file ready to be downloaded. The file can be downloaded via <see cref="TelegramBotClient.DownloadFileAsync"/>. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClientExtensions.GetFileAsync">GetFile</see>.</summary>
/// <remarks>The maximum file size to download is 20 MB</remarks>
public partial class File : FileBase
{
    /// <summary><em>Optional</em>. File path. Use <see cref="TelegramBotClient.DownloadFileAsync"/> to get the file.</summary>
    public string? FilePath { get; set; }
}
