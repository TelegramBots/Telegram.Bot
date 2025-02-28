// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a file ready to be downloaded. The file can be downloaded via <see cref="TelegramBotClient.DownloadFile">DownloadFile</see>. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClientExtensions.GetFile">GetFile</see>.</summary>
/// <remarks>The maximum file size to download is 20 MB</remarks>
public partial class TGFile : FileBase
{
    /// <summary><em>Optional</em>. File path. Use <see cref="TelegramBotClient.DownloadFile">DownloadFile</see> to get the file.</summary>
    [JsonPropertyName("file_path")]
    public string? FilePath { get; set; }
}
