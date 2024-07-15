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
        [
            new InlineQueryResultMpeg4Gif
            {
                Id = "mpeg4_gif_result_with_video_thumb",
                Mpeg4Url = "https://pixabay.com/en/videos/download/video-14205_tiny.mp4",
                ThumbnailUrl = "https://pixabay.com/en/videos/download/video-14205_tiny.mp4",
                Caption = "A frozing bubble",
                ThumbnailMimeType = "video/mp4"
            }
        ];

        AnswerInlineQueryRequest request = new()
        {
            InlineQueryId = "query_id",
            Results = results,
            CacheTime = 0
        };
        string json = JsonSerializer.Serialize(request, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal("query_id", (string?)j["inline_query_id"]);
        Assert.Equal(0, (long?)j["cache_time"]);

        JsonArray ja = Assert.IsAssignableFrom<JsonArray>(j["results"]);
        Assert.Single(ja);
        JsonNode? element = ja[0];
        Assert.NotNull(element);
        Assert.Equal("video/mp4", (string?)element["thumbnail_mime_type"]);
        Assert.Equal("mpeg4_gif_result_with_video_thumb", (string?)element["id"]);
        Assert.Equal("A frozing bubble", (string?)element["caption"]);
        Assert.Equal("https://pixabay.com/en/videos/download/video-14205_tiny.mp4", (string?)element["mpeg4_url"]);
        Assert.Equal("https://pixabay.com/en/videos/download/video-14205_tiny.mp4", (string?)element["thumbnail_url"]);
    }
    [Fact(DisplayName = "Should serialize InlineQueryResultGif with ThumbMimeType")]
    public void Should_Serialize_InlineQueryResultGif_With_ThumbMimeType()
    {
        InlineQueryResult[] results =
        [
            new InlineQueryResultGif
            {
                Id = "gif_result_with_video_thumb",
                GifUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                ThumbnailUrl = "https://pixabay.com/en/videos/download/video-14205_tiny.mp4",
                Caption = "A frozing bubble",
                ThumbnailMimeType = "video/mp4"
            }
        ];

        AnswerInlineQueryRequest request = new()
        {
            InlineQueryId = "query_id",
            Results = results,
            CacheTime = 0
        };
        string json = JsonSerializer.Serialize(request, JsonBotAPI.Options);

        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal("query_id", (string?)j["inline_query_id"]);
        Assert.Equal(0, (long?)j["cache_time"]);

        JsonArray ja = Assert.IsAssignableFrom<JsonArray>(j["results"]);
        Assert.Single(ja);

        JsonNode? element = ja[0];
        Assert.NotNull(element);
        Assert.Equal("video/mp4", (string?)element["thumbnail_mime_type"]);
        Assert.Equal("gif_result_with_video_thumb", (string?)element["id"]);
        Assert.Equal("A frozing bubble", (string?)element["caption"]);
        Assert.Equal("https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif", (string?)element["gif_url"]);
        Assert.Equal("https://pixabay.com/en/videos/download/video-14205_tiny.mp4", (string?)element["thumbnail_url"]);
    }
}
