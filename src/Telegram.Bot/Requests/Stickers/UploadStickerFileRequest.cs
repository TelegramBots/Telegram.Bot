using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using File = Telegram.Bot.Types.File;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to upload a file with a sticker for later use in the
/// <see cref="CreateNewStickerSetRequest"/> and <see cref="AddStickerToSetRequest"/>
/// methods (the file can be used multiple times).
/// Returns the uploaded <see cref="File"/> on success.
/// </summary>
public class UploadStickerFileRequest : FileRequestBase<File>, IUserTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputFileStream Sticker { get; init; }

    /// <summary>
    /// Format of the sticker
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required StickerFormat StickerFormat { get; init; }

    /// <summary>
    /// Initializes a new request with userId, sticker and stickerFormat
    /// </summary>
    /// <param name="userId">
    /// User identifier of sticker file owner
    /// </param>
    /// <param name="sticker">
    /// A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format.
    /// </param>
    /// <param name="stickerFormat">
    /// Format of the sticker
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public UploadStickerFileRequest(long userId, InputFileStream sticker, StickerFormat stickerFormat)
        : this()
    {
        UserId = userId;
        Sticker = sticker;
        StickerFormat = stickerFormat;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public UploadStickerFileRequest()
        : base("uploadStickerFile")
    { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent()
        => ToMultipartFormDataContent(fileParameterName: "sticker", inputFile: Sticker);
}
