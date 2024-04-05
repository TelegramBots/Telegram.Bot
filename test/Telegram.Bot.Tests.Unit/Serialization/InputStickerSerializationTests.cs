using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        InputSticker inputSticker = new(inputFile, emojiList);

        string json = JsonConvert.SerializeObject(inputSticker);
        JObject j = JObject.Parse(json);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal($"attach://{fileName}", j["sticker"]);

        JToken? je = j["emoji_list"];
        Assert.NotNull(je);

        JArray jEmojiList = Assert.IsType<JArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", jEmojiList[0]);
    }

    [Fact(DisplayName = "Should serialize & deserialize input sticker with input file id")]
    public void Should_Serialize_FileId()
    {
        const string fileId = "This-is-a-file_id";
        InputFileId inputFileId = new(fileId);
        string[] emojiList = ["🙂"];
        InputSticker inputStickerFileId = new(inputFileId, emojiList);

        string json = JsonConvert.SerializeObject(inputStickerFileId);
        JObject j = JObject.Parse(json);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal("This-is-a-file_id", j["sticker"]);

        JToken? je = j["emoji_list"];
        Assert.NotNull(je);

        JArray jEmojiList = Assert.IsType<JArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", jEmojiList[0]);
    }

    [Fact(DisplayName = "Should serialize & deserialize input sticker with input file URL")]
    public void Should_Serialize_InputUrlFile()
    {
        Uri url = new("https://github.com/TelegramBots");
        InputFileUrl inputFileUrl = new(url);
        string[] emojiList = ["🙂"];
        InputSticker inputStickerFileUrl = new(inputFileUrl, emojiList);

        string json = JsonConvert.SerializeObject(inputStickerFileUrl);
        JObject j = JObject.Parse(json);

        Assert.Equal(3, j.Children().Count());
        Assert.Equal("https://github.com/TelegramBots", j["sticker"]);

        JToken? je = j["emoji_list"];
        Assert.NotNull(je);
        JArray jEmojiList = Assert.IsType<JArray>(je);
        Assert.Single(jEmojiList);
        Assert.Equal("🙂", jEmojiList[0]);
    }
}
