using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatActionConverterTests
{
    [Theory]
    [ClassData(typeof(ChatActionData))]
    public void Should_Convert_ChatAction_To_String(SendChatActionRequest sendChatActionRequest, string value)
    {
        string expectedResult = $$"""{"chat_id":1234,"action":"{{value}}"}""";

        string result =
            JsonSerializer.Serialize(sendChatActionRequest, TelegramBotClientJsonSerializerContext.Instance.SendChatActionRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(ChatActionData))]
    public void Should_Convert_String_ToChatAction(SendChatActionRequest expectedResult, string value)
    {
        string jsonData = $$"""{"chat_id":1234,"action":"{{value}}"}""";

        SendChatActionRequest? result =
            JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.SendChatActionRequest);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Action, result.Action);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatAction()
    {
        ChatAction? result =
            JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatAction);

        Assert.NotNull(result);
        Assert.Equal((ChatAction)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatAction()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((ChatAction)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatAction));
    }

    private class ChatActionData : IEnumerable<object[]>
    {
        private static SendChatActionRequest NewSendChatActionRequest(ChatAction chatAction)
        {
               return new SendChatActionRequest
                {
                    ChatId = 1234,
                    Action = chatAction
                };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewSendChatActionRequest(ChatAction.Typing), "typing"];
            yield return [NewSendChatActionRequest(ChatAction.UploadPhoto), "upload_photo"];
            yield return [NewSendChatActionRequest(ChatAction.RecordVideo), "record_video"];
            yield return [NewSendChatActionRequest(ChatAction.UploadVideo), "upload_video"];
            yield return [NewSendChatActionRequest(ChatAction.RecordVoice), "record_voice"];
            yield return [NewSendChatActionRequest(ChatAction.UploadVoice), "upload_voice"];
            yield return [NewSendChatActionRequest(ChatAction.UploadDocument), "upload_document"];
            yield return [NewSendChatActionRequest(ChatAction.ChooseSticker), "choose_sticker"];
            yield return [NewSendChatActionRequest(ChatAction.FindLocation), "find_location"];
            yield return [NewSendChatActionRequest(ChatAction.RecordVideoNote), "record_video_note"];
            yield return [NewSendChatActionRequest(ChatAction.UploadVideoNote), "upload_video_note"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
