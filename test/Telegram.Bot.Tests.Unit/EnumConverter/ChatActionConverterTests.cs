using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatActionConverterTests
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
    public void Should_Convert_ChatAction_To_String(ChatAction chatAction, string value)
    {
        SendChatActionRequest sendChatActionRequest = new() { Type = chatAction };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(sendChatActionRequest, JsonSerializerOptionsProvider.Options);

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
    public void Should_Convert_String_ToChatAction(ChatAction chatAction, string value)
    {
        SendChatActionRequest expectedResult = new() { Type = chatAction };
        string jsonData = @$"{{""type"":""{value}""}}";

        SendChatActionRequest? result = JsonSerializer.Deserialize<SendChatActionRequest>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatAction()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        SendChatActionRequest? result = JsonSerializer.Deserialize<SendChatActionRequest>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((ChatAction)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatAction()
    {
        SendChatActionRequest sendChatActionRequest = new() { Type = (ChatAction)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(sendChatActionRequest, JsonSerializerOptionsProvider.Options));
    }


    class SendChatActionRequest
    {
        [JsonRequired]
        public ChatAction Type { get; init; }
    }
}
