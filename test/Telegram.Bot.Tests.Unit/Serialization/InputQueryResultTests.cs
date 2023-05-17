using Newtonsoft.Json;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputQueryResultTests
{
    [Fact(DisplayName = "Should serialize InlineQueryResultMpeg4Gif with ThumbMimeType")]
    public void Should_Serialize_InlineQueryResultMpeg4Gif_With_ThumbMimeType()
    {
        InlineQueryResult[] results =
        {
            new InlineQueryResultMpeg4Gif(
                id: "mpeg4_gif_result_with_video_thumb",
                mpeg4Url: "https://pixabay.com/en/videos/download/video-14205_tiny.mp4",
                thumbnailUrl: "https://pixabay.com/en/videos/download/video-14205_tiny.mp4")
            {
                Caption = "A frozing bubble",
                ThumbnailMimeType = "video/mp4"
            },
        };

        AnswerInlineQueryRequest request = new("query_id", results) { CacheTime = 0 };
        string json = JsonConvert.SerializeObject(request);

        Assert.Contains(@"""thumbnail_mime_type"":""video/mp4""", json);
    }
    [Fact(DisplayName = "Should serialize InlineQueryResultGif with ThumbMimeType")]
    public void Should_Serialize_InlineQueryResultGif_With_ThumbMimeType()
    {
        InlineQueryResult[] results =
        {
            new InlineQueryResultGif(
                id: "gif_result_with_video_thumb",
                gifUrl: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                thumbnailUrl: "https://pixabay.com/en/videos/download/video-14205_tiny.mp4")
            {
                Caption = "A frozing bubble",
                ThumbnailMimeType = "video/mp4"
            },
        };

        AnswerInlineQueryRequest request = new("query_id", results) { CacheTime = 0 };
        string json = JsonConvert.SerializeObject(request);

        Assert.Contains(@"""thumbnail_mime_type"":""video/mp4""", json);
    }
}
