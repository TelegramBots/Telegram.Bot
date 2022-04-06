using System;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ChatActionConverter : EnumConverter<ChatAction>
{
    static readonly IReadOnlyDictionary<string, ChatAction> StringToEnum =
        new Dictionary<string, ChatAction>
        {
            {"typing", ChatAction.Typing},
            {"upload_photo", ChatAction.UploadPhoto},
            {"record_video", ChatAction.RecordVideo},
            {"upload_video", ChatAction.UploadVideo},
            {"record_voice", ChatAction.RecordVoice},
            {"upload_voice", ChatAction.UploadVoice},
            {"upload_document", ChatAction.UploadDocument},
            {"find_location",ChatAction.FindLocation},
            {"record_video_note", ChatAction.RecordVideoNote},
            {"upload_video_note", ChatAction.UploadVideoNote},
            {"choose_sticker", ChatAction.ChooseSticker},
        };

    static readonly IReadOnlyDictionary<ChatAction, string> EnumToString =
        new Dictionary<ChatAction, string>
        {
            { 0, "unknown" },
            {ChatAction.Typing, "typing"},
            {ChatAction.UploadPhoto, "upload_photo"},
            {ChatAction.RecordVideo, "record_video"},
            {ChatAction.UploadVideo, "upload_video"},
            {ChatAction.RecordVoice, "record_voice"},
            {ChatAction.UploadVoice, "upload_voice"},
            {ChatAction.UploadDocument, "upload_document"},
            {ChatAction.FindLocation, "find_location"},
            {ChatAction.RecordVideoNote, "record_video_note"},
            {ChatAction.UploadVideoNote, "upload_video_note"},
            {ChatAction.ChooseSticker, "choose_sticker" },
        };

    protected override ChatAction GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(ChatAction value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : throw new NotSupportedException();
}
