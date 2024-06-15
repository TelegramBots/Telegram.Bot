using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new sticker set owned by a user.
/// The bot will be able to edit the sticker set thus created.
/// Returns <see langword="true"/> on success.
/// </summary>
public class CreateNewStickerSetRequest : FileRequestBase<bool>, IUserTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only English letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; init; }

    /// <summary>
    /// Sticker set title, 1-64 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// A list of 1-50 initial stickers to be added to the sticker set
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<InputSticker> Stickers { get; init; }

    /// <summary>
    /// Format of stickers in the set.
    /// </summary>
    [Obsolete("This property is no longer recognised by Telegram")]
    public StickerFormat StickerFormat { get; init; }

    /// <summary>
    /// Type of stickers in the set.
    /// By default, a regular sticker set is created.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StickerType? StickerType { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if stickers in the sticker set must be repainted to the
    /// color of text when used in messages, the accent color if used as emoji status, white
    /// on chat photos, or another appropriate color based on context;
    /// for <see cref="StickerType.CustomEmoji">custom emoji</see> sticker sets only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? NeedsRepainting { get; set; }

    /// <summary>
    /// Initializes a new request with userId, name, title, stickers and stickerFormat
    /// </summary>
    /// <param name="userId">
    /// User identifier of sticker set owner
    /// </param>
    /// <param name="name">
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>).
    /// Can contain only english letters, digits and underscores. Must begin with a letter, can't
    /// contain consecutive underscores and must end in <i>"_by_&lt;bot username&gt;"</i>.
    /// <i>&lt;bot_username&gt;</i> is case insensitive. 1-64 characters
    /// </param>
    /// <param name="title">
    /// Sticker set title, 1-64 characters
    /// </param>
    /// <param name="stickers">
    /// A list of 1-50 initial stickers to be added to the sticker set
    /// </param>
    /// <param name="stickerFormat">
    /// Format of stickers in the set.
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public CreateNewStickerSetRequest(
        long userId,
        string name,
        string title,
        IEnumerable<InputSticker> stickers,
        StickerFormat stickerFormat)
        : this()
    {
        UserId = userId;
        Name = name;
        Title = title;
        Stickers = stickers;
        StickerFormat = stickerFormat;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public CreateNewStickerSetRequest()
        : base("createNewStickerSet")
    { }

    /// <inheritdoc/>
    public override HttpContent ToHttpContent()
    {
        var multipartContent = GenerateMultipartFormDataContent();

        foreach (var inputSticker in Stickers)
        {
            if (inputSticker is { Sticker: InputFileStream file })
            {
                multipartContent.AddContentIfInputFile(file, file.FileName!);
            }
        }

        return multipartContent;
    }
}
