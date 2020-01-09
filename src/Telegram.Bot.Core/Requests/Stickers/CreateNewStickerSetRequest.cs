﻿using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Create new sticker set owned by a user. The bot will be able to edit the created sticker set. Returns True on success.
    /// </summary>
    public class CreateNewStickerSetRequest : FileRequestBase<bool>
    {
        /// <summary>
        /// User identifier of sticker set owner
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only english letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_[bot username]”. [bot_username] is case insensitive. 1-64 characters.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Sticker set title, 1-64 characters
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.
        /// </summary>
        public InputOnlineFile PngSticker { get; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        public string Emojis { get; }

        /// <summary>
        /// Pass True, if a set of mask stickers should be created
        /// </summary>
        public bool ContainsMasks { get; set; }

        /// <summary>
        /// One or more emoji corresponding to the sticker
        /// </summary>
        public MaskPosition MaskPosition { get; set; }

        /// <summary>
        /// Initializes a new request with userId, name, pngSticker and emojis
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="pngSticker">Png image with the sticker</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public CreateNewStickerSetRequest(int userId, string name, string title, InputOnlineFile pngSticker,
                                          string emojis)
            : base("createNewStickerSet")
        {
            UserId = userId;
            Name = name;
            Title = title;
            PngSticker = pngSticker;
            Emojis = emojis;
        }

        /// <param name="jsonConverter"></param>
        /// <param name="cancellationToken"></param>
        /// <inheritdoc />
        public override async ValueTask<HttpContent> ToHttpContentAsync(
            [DisallowNull] ITelegramBotJsonConverter jsonConverter,
            CancellationToken cancellationToken) =>
            PngSticker.FileType == FileType.Stream
                ? await ToMultipartFormDataContentAsync(jsonConverter, "png_sticker", PngSticker, cancellationToken)
                : await base.ToHttpContentAsync(jsonConverter, cancellationToken);
    }
}
