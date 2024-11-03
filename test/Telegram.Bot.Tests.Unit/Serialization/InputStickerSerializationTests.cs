using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputStickerSerializationTests
{
    [Fact(DisplayName = "Should serialize & deserialize input sticker from input file stream")]
    public void Should_Serialize_InputFile()
    {
        const string fileName = "myFile";
        InputFileStream inputFile = new(new MemoryStream(), fileName);
        string[] emojiList = ["🙂"];
        InputSticker inputSticker = new()
        {
            Sticker = inputFile,
            EmojiList = emojiList,
            Format = StickerFormat.Static
        };

        string json = JsonSerializer.Serialize(inputSticker, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal($"attach://0", (string?)j["sticker"]);
        Assert.Equal("static", (string?)j["format"]);

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
        InputSticker inputStickerFileId = new()
        {
            Sticker = inputFileId,
            EmojiList = emojiList,
            Format = StickerFormat.Static,
        };

        string json = JsonSerializer.Serialize(inputStickerFileId, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);
        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);

        Assert.Equal(3, j.Count);
        Assert.Equal("This-is-a-file_id", (string?)j["sticker"]);
        Assert.Equal("static", (string?)j["format"]);

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
        InputSticker inputStickerFileUrl = new()
        {
            Sticker = inputFileUrl,
            EmojiList = emojiList,
            Format = StickerFormat.Static,
        };

        string json = JsonSerializer.Serialize(inputStickerFileUrl, JsonBotAPI.Options);
        JsonNode? root = JsonNode.Parse(json);
        Assert.NotNull(root);

        JsonObject j = Assert.IsAssignableFrom<JsonObject>(root);
        Assert.Equal(3, j.Count);
        Assert.Equal("https://github.com/TelegramBots", (string?)j["sticker"]);
        Assert.Equal("static", (string?)j["format"]);

        JsonNode? je = j["emoji_list"];
        Assert.NotNull(je);
        JsonArray jEmojiList = Assert.IsAssignableFrom<JsonArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", (string?)jEmojiList[0]);
    }
}
