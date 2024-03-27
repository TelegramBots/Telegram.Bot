using System.Threading;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a file ready to be downloaded. The file can be downloaded via
/// <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/>.
/// It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested
/// by calling <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/>.
/// </summary>
public class File : FileBase
{
    /// <summary>
    /// Optional. File path. Use
    /// <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/>
    /// to get the file.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FilePath { get; set; }
}
