using System.IO;
using Telegram.Bot.Types;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputStickerSerializationTests
{
    [Fact(DisplayName = "Should serialize & deserialize input sticker from input file stream")]
    public void Should_Serialize_InputFile()
    {
        const string fileName = "myFile";
        InputFileStream inputFile = new(new MemoryStream(), fileName);
        string[] emojiList = ["🙂"];
        InputSticker inputSticker = new(inputFile, emojiList);

        string json = JsonSerializer.Serialize(inputSticker, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal($"attach://{fileName}", (string?)j["sticker"]);

        JsonNode? je = j["emoji_list"];
        Assert.NotNull(je);

        JsonArray jEmojiList = Assert.IsAssignableFrom<JsonArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", (string?)jEmojiList[0]);
    }

    [Fact(DisplayName = "Should serialize & deserialize input sticker with input file id")]
    public void Should_Serialize_FileId()
    {
        const string fileId = "This-is-a-file_id";
        InputFileId inputFileId = new(fileId);
        string[] emojiList = ["🙂"];
        InputSticker inputStickerFileId = new(inputFileId, emojiList);

        string json = JsonSerializer.Serialize(inputStickerFileId, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal("This-is-a-file_id", (string?)j["sticker"]);

        JsonNode? je = j["emoji_list"];
        Assert.NotNull(je);

        JsonArray jEmojiList = Assert.IsAssignableFrom<JsonArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", (string?)jEmojiList[0]);
    }

    [Fact(DisplayName = "Should serialize & deserialize input sticker with input file URL")]
    public void Should_Serialize_InputUrlFile()
    {
        Uri url = new("https://github.com/TelegramBots");
        InputFileUrl inputFileUrl = new(url);
        string[] emojiList = ["🙂"];
        InputSticker inputStickerFileUrl = new(inputFileUrl, emojiList);

        string json = JsonSerializer.Serialize(inputStickerFileUrl, JsonSerializerOptionsProvider.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal("https://github.com/TelegramBots", (string?)j["sticker"]);

        JsonNode? je = j["emoji_list"];
        Assert.NotNull(je);
        JsonArray jEmojiList = Assert.IsAssignableFrom<JsonArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", (string?)jEmojiList[0]);
    }
}
