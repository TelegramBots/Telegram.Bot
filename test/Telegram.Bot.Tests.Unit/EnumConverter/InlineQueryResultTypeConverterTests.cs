using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class InlineQueryResultTypeConverterTests
{
    [Theory]
    [ClassData(typeof(InlineQueryResultData))]
    public void Should_Convert_InlineQueryResultType_To_String(InlineQueryResult inlineQuery, string value)
    {
        string result = JsonSerializer.Serialize(inlineQuery, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResult);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(InlineQueryResultData))]
    public void Should_Convert_String_To_InlineQueryResultType(InlineQueryResult expectedResult, string value)
    {
        InlineQueryResult? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResult);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_InlineQueryResultType()
    {
        InlineQueryResultType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultType);

        Assert.NotNull(result);
        Assert.Equal((InlineQueryResultType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_InlineQueryResultType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((InlineQueryResultType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultType));
    }

    private class InlineQueryResultData : IEnumerable<object[]>
    {
        private static InlineQueryResult NewInlineQueryResult(InlineQueryResultType inlineQueryResultType)
        {
            return inlineQueryResultType switch
            {
                InlineQueryResultType.Article => new InlineQueryResultArticle
                {
                    Id = "id",
                    Title = "title",
                    InputMessageContent = new InputTextMessageContent { MessageText = "message" },
                },
                InlineQueryResultType.Photo => new InlineQueryResultPhoto
                {
                    Id = "id",
                    PhotoUrl = "url",
                    ThumbnailUrl = "thumb",
                },
                InlineQueryResultType.Gif => new InlineQueryResultGif
                {
                    Id = "id",
                    GifUrl = "url",
                    ThumbnailUrl = "thumb",
                },
                InlineQueryResultType.Mpeg4Gif => new InlineQueryResultMpeg4Gif
                {
                    Id = "id",
                    Mpeg4Url = "url",
                    ThumbnailUrl = "thumb",
                },
                InlineQueryResultType.Video => new InlineQueryResultVideo
                {
                    Id = "id",
                    VideoUrl = "url",
                    MimeType = "video/mp4",
                    ThumbnailUrl = "thumb",
                    Title = "title",
                },
                InlineQueryResultType.Audio => new InlineQueryResultAudio
                {
                    Id = "id",
                    AudioUrl = "url",
                    Title = "title",
                },
                InlineQueryResultType.Contact => new InlineQueryResultContact
                {
                    Id = "id",
                    PhoneNumber = "123456789",
                    FirstName = "FirstName",
                },
                InlineQueryResultType.Document => new InlineQueryResultDocument
                {
                    Id = "id",
                    Title = "title",
                    DocumentUrl = "url",
                    MimeType = "application/pdf",
                },
                InlineQueryResultType.Location => new InlineQueryResultLocation
                {
                    Id = "id",
                    Latitude = 1.0f,
                    Longitude = 1.0f,
                    Title = "title",
                },
                InlineQueryResultType.Venue => new InlineQueryResultVenue
                {
                    Id = "id",
                    Latitude = 1.0f,
                    Longitude = 1.0f,
                    Title = "title",
                    Address = "address",
                },
                InlineQueryResultType.Voice => new InlineQueryResultVoice
                {
                    Id = "id",
                    VoiceUrl = "url",
                    Title = "title",
                },
                InlineQueryResultType.Game => new InlineQueryResultGame
                {
                    Id = "id",
                    GameShortName = "game",
                },
                InlineQueryResultType.Sticker => new InlineQueryResultCachedSticker
                {
                    Id = "id",
                    StickerFileId = "file_id",
                },
                _ => throw new ArgumentOutOfRangeException(nameof(inlineQueryResultType), inlineQueryResultType, null)
            };

        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewInlineQueryResult(InlineQueryResultType.Article), """{"type":"article","title":"title","input_message_content":{"message_text":"message"},"id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Photo), """{"type":"photo","photo_url":"url","thumbnail_url":"thumb","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Gif), """{"type":"gif","gif_url":"url","thumbnail_url":"thumb","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Mpeg4Gif), """{"type":"mpeg4_gif","mpeg4_url":"url","thumbnail_url":"thumb","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Video), """{"type":"video","video_url":"url","mime_type":"video/mp4","thumbnail_url":"thumb","title":"title","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Audio), """{"type":"audio","audio_url":"url","title":"title","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Contact), """{"type":"contact","phone_number":"123456789","first_name":"FirstName","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Document), """{"type":"document","title":"title","document_url":"url","mime_type":"application/pdf","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Location), """{"type":"location","latitude":1,"longitude":1,"title":"title","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Venue), """{"type":"venue","latitude":1,"longitude":1,"title":"title","address":"address","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Voice), """{"type":"voice","voice_url":"url","title":"title","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Game), """{"type":"game","game_short_name":"game","id":"id"}"""];
            yield return [NewInlineQueryResult(InlineQueryResultType.Sticker), """{"type":"sticker","sticker_file_id":"file_id","id":"id"}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
