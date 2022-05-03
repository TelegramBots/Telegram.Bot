using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_ChatActionConverter
{
    [Theory]
    [InlineData(ChatAction.Typing, "typing")]
    [InlineData(ChatAction.UploadPhoto, "upload_photo")]
    [InlineData(ChatAction.RecordVideo, "record_video")]
    [InlineData(ChatAction.UploadVideo, "upload_video")]
    [InlineData(ChatAction.RecordVoice, "record_voice")]
    [InlineData(ChatAction.UploadVoice, "upload_voice")]
    [InlineData(ChatAction.UploadDocument, "upload_document")]
    [InlineData(ChatAction.ChooseSticker, "choose_sticker")]
    [InlineData(ChatAction.FindLocation, "find_location")]
    [InlineData(ChatAction.RecordVideoNote, "record_video_note")]
    [InlineData(ChatAction.UploadVideoNote, "upload_video_note")]
    public void Sould_Convert_ChatAction_To_String(ChatAction chatAction, string value)
    {
        SendChatActionRequest sendChatActionRequest = new SendChatActionRequest() { Type = chatAction };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(sendChatActionRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ChatAction.Typing, "typing")]
    [InlineData(ChatAction.UploadPhoto, "upload_photo")]
    [InlineData(ChatAction.RecordVideo, "record_video")]
    [InlineData(ChatAction.UploadVideo, "upload_video")]
    [InlineData(ChatAction.RecordVoice, "record_voice")]
    [InlineData(ChatAction.UploadVoice, "upload_voice")]
    [InlineData(ChatAction.UploadDocument, "upload_document")]
    [InlineData(ChatAction.ChooseSticker, "choose_sticker")]
    [InlineData(ChatAction.FindLocation, "find_location")]
    [InlineData(ChatAction.RecordVideoNote, "record_video_note")]
    [InlineData(ChatAction.UploadVideoNote, "upload_video_note")]
    public void Sould_Convert_String_ToChatAction(ChatAction chatAction, string value)
    {
        SendChatActionRequest expectedResult = new SendChatActionRequest() { Type = chatAction };
        string jsonData = @$"{{""type"":""{value}""}}";

        SendChatActionRequest result = JsonConvert.DeserializeObject<SendChatActionRequest>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_ChatAction()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        SendChatActionRequest result = JsonConvert.DeserializeObject<SendChatActionRequest>(jsonData);

        Assert.Equal((ChatAction)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_ChatAction()
    {
        SendChatActionRequest sendChatActionRequest = new SendChatActionRequest() { Type = (ChatAction)int.MaxValue };

        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(sendChatActionRequest));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class SendChatActionRequest
    {
        [JsonProperty(Required = Required.Always)]
        public ChatAction Type { get; init; }
    }
}
