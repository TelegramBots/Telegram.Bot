﻿using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to create a new static sticker set owned by a user. The bot will be able to edit
/// the sticker set thus created. Returns <c>true</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CreateNewStaticStickerSetRequest : CreateNewStickerSetRequest
{
    /// <summary>
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must
    /// not exceed 512px, and either width or height must be exactly 512px. Pass a
    /// <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that
    /// already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to
    /// get a file from the Internet, or upload a new one using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public InputFileStream PngSticker { get; }

    /// <inheritdoc />
    /// <param name="pngSticker">
    /// <b>PNG</b> image with the sticker, must be up to 512 kilobytes in size, dimensions must
    /// not exceed 512px, and either width or height must be exactly 512px. Pass a
    /// <see cref="Types.InputFiles.InputTelegramFile.FileId"/> as a String to send a file that
    /// already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to
    /// get a file from the Internet, or upload a new one using multipart/form-data
    /// </param>
#pragma warning disable CS1573
    public CreateNewStaticStickerSetRequest(
        long userId,
        string name,
        string title,
        InputOnlineFile pngSticker,
        string emojis) : base(userId, name, title, emojis)
    {
        PngSticker = pngSticker ?? throw new ArgumentNullException(nameof(pngSticker), "Sticker is null");
    }
#pragma warning restore CS1573

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        PngSticker.FileType == FileType.Stream
            ? ToMultipartFormDataContent(fileParameterName: "png_sticker", inputFile: PngSticker)
            : base.ToHttpContent();
}