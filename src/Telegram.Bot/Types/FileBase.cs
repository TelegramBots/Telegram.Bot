using System.Threading;
using Telegram.Bot.Requests;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a file ready to be downloaded. The file can be downloaded via
/// <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/>.
/// It is guaranteed that the link will be valid for
/// at least 1 hour. When the link expires, a new one can be requested by calling
/// <see cref="TelegramBotClientExtensions.GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/>.
/// </summary>
public abstract class FileBase
{
    /// <summary>
    /// Identifier for this file, which can be used to download or reuse the file
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FileId { get; set; } = default!;

    /// <summary>
    /// Unique identifier for this file, which is supposed to be the same over time and for different bots.
    /// Can't be used to download or reuse the file.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FileUniqueId { get; set; } = default!;

    /// <summary>
    /// Optional. File size
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? FileSize { get; set; }
}
