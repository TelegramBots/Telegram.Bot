using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Stickers;

[Collection(Constants.TestCollections.Stickers)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class StickersTests(TestsFixture fixture, StickersTestsFixture classFixture)
    : TestClass(fixture), IClassFixture<StickersTestsFixture>
{
    #region 1. Upload sticker files
    [OrderedFact("Should upload static sticker file")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UploadStickerFile)]
    public async Task Should_Upload_Static_Sticker_File()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Sticker.Regular.StaticFirst);

        TGFile file = await BotClient.UploadStickerFile(
            userId: classFixture.OwnerUserId,
            sticker: new(stream),
            stickerFormat: StickerFormat.Static
        );

        Assert.NotEmpty(file.FileId);
        Assert.True(file.FileSize > 0);

        classFixture.TestUploadedStaticStickerFile = file;
    }

    [OrderedFact("Should upload animated sticker file")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UploadStickerFile)]
    public async Task Should_Upload_Animated_Sticker_File()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Sticker.Regular.AnimatedFirst);

        TGFile file = await BotClient.UploadStickerFile(
            userId: classFixture.OwnerUserId,
            sticker: new(stream),
            stickerFormat: StickerFormat.Animated
        );

        Assert.NotEmpty(file.FileId);
        Assert.True(file.FileSize > 0);

        classFixture.TestUploadedAnimatedStickerFile = file;
    }

    [OrderedFact("Should upload video sticker file")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UploadStickerFile)]
    public async Task Should_Upload_Video_Sticker_File()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Sticker.Regular.VideoFirst);

        TGFile file = await BotClient.UploadStickerFile(
            userId: classFixture.OwnerUserId,
            sticker: new(stream),
            stickerFormat: StickerFormat.Video
        );

        Assert.NotEmpty(file.FileId);
        Assert.True(file.FileSize > 0);

        classFixture.TestUploadedVideoStickerFile = file;
    }
    #endregion

    #region 2. Create sticker sets
    [OrderedFact("Should create new static sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Create_New_Static_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.StaticSecond
        );

        List<InputSticker> inputStickers =
        [
            new()
            {
                Sticker = InputFile.FromFileId(classFixture.TestUploadedStaticStickerFile.FileId),
                EmojiList = classFixture.FirstEmojis,
                Format = StickerFormat.Static,
            },
            new()
            {
                Sticker = InputFile.FromStream(stream, "Static2.webp"),
                EmojiList = classFixture.SecondEmojis,
                Format = StickerFormat.Static,
            },
        ];

        await BotClient.WithStreams(stream).CreateNewStickerSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticRegularStickerSetName,
            title: classFixture.TestStickerSetTitle,
            stickers: inputStickers,
            stickerType: StickerType.Regular
        );

        await Task.Delay(1_000);

        classFixture.TestStaticRegularStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.Equal(2, classFixture.TestStaticRegularStickerSet.Stickers.Length);
    }

    [OrderedFact("Should create new animated sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Create_New_Animated_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.AnimatedSecond
        );

        List<InputSticker> inputStickers = [
            new()
            {
                Sticker = InputFile.FromFileId(classFixture.TestUploadedAnimatedStickerFile.FileId),
                EmojiList = classFixture.FirstEmojis,
                Format = StickerFormat.Animated,
            },
            new()
            {
                Sticker = InputFile.FromStream(stream, "Animated2.webp"),
                EmojiList = classFixture.SecondEmojis,
                Format = StickerFormat.Animated,
            },
        ];

        await BotClient.WithStreams(stream).CreateNewStickerSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestAnimatedRegularStickerSetName,
            title: classFixture.TestStickerSetTitle,
            stickers: inputStickers,
            stickerType: StickerType.Regular
        );

        await Task.Delay(1_000);

        classFixture.TestAnimatedRegularStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestAnimatedRegularStickerSetName
        );

        Assert.Equal(2, classFixture.TestAnimatedRegularStickerSet.Stickers.Length);
    }

    [OrderedFact("Should create new video sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Create_New_Video_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.VideoSecond
        );


        List<InputSticker> inputStickers = [
            new()
            {
                Sticker = InputFile.FromFileId(classFixture.TestUploadedVideoStickerFile.FileId),
                EmojiList = classFixture.FirstEmojis,
                Format = StickerFormat.Video,
            },
            new()
            {
                Sticker = InputFile.FromStream(stream, "Video2.webp"),
                EmojiList = classFixture.SecondEmojis,
                Format = StickerFormat.Video,
            },
        ];
        await BotClient.WithStreams(stream).CreateNewStickerSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestVideoRegularStickerSetName,
            title: classFixture.TestStickerSetTitle,
            stickers: inputStickers,
            stickerType: StickerType.Regular
        );

        await Task.Delay(1_000);

        classFixture.TestVideoRegularStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestVideoRegularStickerSetName
        );

        Assert.Equal(2, classFixture.TestVideoRegularStickerSet.Stickers.Length);
    }
    #endregion

    #region 3. Get and send stickers from a set
    [OrderedFact("Should send static sticker")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendSticker)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Send_Static_Sticker()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.Equal(2, stickerSet.Stickers.Length);

        Sticker firstSticker = stickerSet.Stickers.First();

        string firstEmojisString = string.Concat(classFixture.FirstEmojis);

        Assert.Equal(firstEmojisString, firstSticker.Emoji);

        Message stickerMessage = await BotClient.SendSticker(
            chatId: Fixture.SupergroupChat.Id,
            sticker: InputFile.FromFileId(firstSticker.FileId)
        );

        Assert.Equal(MessageType.Sticker, stickerMessage.Type);
        Assert.NotNull(stickerMessage.Sticker);
        Assert.Equal(firstSticker.FileUniqueId, stickerMessage.Sticker.FileUniqueId);
        Assert.Equal(firstSticker.FileSize, stickerMessage.Sticker.FileSize);
        Assert.Equal(firstSticker.Type, stickerMessage.Sticker.Type);
        Assert.Equal(firstSticker.Width, stickerMessage.Sticker.Width);
        Assert.Equal(firstSticker.Height, stickerMessage.Sticker.Height);
        Assert.False(stickerMessage.Sticker.IsAnimated);
        Assert.False(stickerMessage.Sticker.IsVideo);
        Assert.NotNull(firstSticker.Thumbnail);
        Assert.NotNull(stickerMessage.Sticker.Thumbnail);
        Assert.Equal(firstSticker.Thumbnail.FileUniqueId, stickerMessage.Sticker.Thumbnail.FileUniqueId);
        Assert.Equal(firstSticker.Thumbnail.FileSize, stickerMessage.Sticker.Thumbnail.FileSize);
        Assert.Equal(firstSticker.Thumbnail.Width, stickerMessage.Sticker.Thumbnail.Width);
        Assert.Equal(firstSticker.Thumbnail.Height, stickerMessage.Sticker.Thumbnail.Height);
        Assert.Equal(firstSticker.Emoji, stickerMessage.Sticker.Emoji);
        Assert.Equal(firstSticker.SetName, stickerMessage.Sticker.SetName);
        Assert.Equal(firstSticker.MaskPosition, stickerMessage.Sticker.MaskPosition);
    }

    [OrderedFact("Should send animated sticker")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendSticker)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Send_Animated_Sticker()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestAnimatedRegularStickerSetName
        );

        Assert.Equal(2, stickerSet.Stickers.Length);

        Sticker firstSticker = stickerSet.Stickers.First();

        string firstEmojisString = string.Concat(classFixture.FirstEmojis);

        Assert.Equal(firstEmojisString, firstSticker.Emoji);

        Message stickerMessage = await BotClient.SendSticker(
            chatId: Fixture.SupergroupChat.Id,
            sticker: InputFile.FromFileId(firstSticker.FileId)
        );

        Assert.Equal(MessageType.Sticker, stickerMessage.Type);
        Assert.NotNull(stickerMessage.Sticker);
        Assert.Equal(firstSticker.FileUniqueId, stickerMessage.Sticker.FileUniqueId);
        Assert.Equal(firstSticker.FileSize, stickerMessage.Sticker.FileSize);
        Assert.Equal(firstSticker.Type, stickerMessage.Sticker.Type);
        Assert.Equal(firstSticker.Width, stickerMessage.Sticker.Width);
        Assert.Equal(firstSticker.Height, stickerMessage.Sticker.Height);
        Assert.True(stickerMessage.Sticker.IsAnimated);
        Assert.False(stickerMessage.Sticker.IsVideo);
        Assert.NotNull(firstSticker.Thumbnail);
        Assert.NotNull(stickerMessage.Sticker.Thumbnail);
        Assert.Equal(firstSticker.Thumbnail.FileUniqueId, stickerMessage.Sticker.Thumbnail.FileUniqueId);
        Assert.Equal(firstSticker.Thumbnail.FileSize, stickerMessage.Sticker.Thumbnail.FileSize);
        Assert.Equal(firstSticker.Thumbnail.Width, stickerMessage.Sticker.Thumbnail.Width);
        Assert.Equal(firstSticker.Thumbnail.Height, stickerMessage.Sticker.Thumbnail.Height);
        Assert.Equal(firstSticker.Emoji, stickerMessage.Sticker.Emoji);
        Assert.Equal(firstSticker.SetName, stickerMessage.Sticker.SetName);
        Assert.Equal(firstSticker.MaskPosition, stickerMessage.Sticker.MaskPosition);
    }

    [OrderedFact("Should send video sticker")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendSticker)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Send_Video_Sticker()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestVideoRegularStickerSetName
        );

        Assert.Equal(2, stickerSet.Stickers.Length);

        Sticker firstSticker = stickerSet.Stickers.First();

        string firstEmojisString = string.Concat(classFixture.FirstEmojis);

        Assert.Equal(firstEmojisString, firstSticker.Emoji);

        Message stickerMessage = await BotClient.SendSticker(
            chatId: Fixture.SupergroupChat.Id,
            sticker: InputFile.FromFileId(firstSticker.FileId)
        );

        Assert.Equal(MessageType.Sticker, stickerMessage.Type);
        Assert.NotNull(stickerMessage.Sticker);
        Assert.Equal(firstSticker.FileUniqueId, stickerMessage.Sticker.FileUniqueId);
        Assert.Equal(firstSticker.FileSize, stickerMessage.Sticker.FileSize);
        Assert.Equal(firstSticker.Type, stickerMessage.Sticker.Type);
        Assert.Equal(firstSticker.Width, stickerMessage.Sticker.Width);
        Assert.Equal(firstSticker.Height, stickerMessage.Sticker.Height);
        Assert.False(stickerMessage.Sticker.IsAnimated);
        Assert.True(stickerMessage.Sticker.IsVideo);
        Assert.NotNull(firstSticker.Thumbnail);
        Assert.NotNull(stickerMessage.Sticker.Thumbnail);
        Assert.Equal(firstSticker.Thumbnail.FileUniqueId, stickerMessage.Sticker.Thumbnail.FileUniqueId);
        Assert.Equal(firstSticker.Thumbnail.FileSize, stickerMessage.Sticker.Thumbnail.FileSize);
        Assert.Equal(firstSticker.Thumbnail.Width, stickerMessage.Sticker.Thumbnail.Width);
        Assert.Equal(firstSticker.Thumbnail.Height, stickerMessage.Sticker.Thumbnail.Height);
        Assert.Equal(firstSticker.Emoji, stickerMessage.Sticker.Emoji);
        Assert.Equal(firstSticker.SetName, stickerMessage.Sticker.SetName);
        Assert.Equal(firstSticker.MaskPosition, stickerMessage.Sticker.MaskPosition);
    }
    #endregion

    #region 4. Add stickers to sets
    [OrderedFact("Should add sticker to static sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Add_Sticker_To_Static_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.StaticThird
        );

        InputSticker inputSticker = new()
        {
            Sticker = InputFile.FromStream(stream, "Static3.png"),
            EmojiList = classFixture.ThirdEmojis,
            Format = StickerFormat.Static,
        };

        await BotClient.WithStreams(stream).AddStickerToSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticRegularStickerSetName,
            sticker: inputSticker
        );

        await Task.Delay(3_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.Equal(3, stickerSet.Stickers.Length);

        Sticker thirdSticker = stickerSet.Stickers[2];

        string thirdEmojisString = string.Concat(classFixture.ThirdEmojis);

        Assert.Equal(thirdEmojisString, thirdSticker.Emoji);
        Assert.False(thirdSticker.IsAnimated);
        Assert.False(thirdSticker.IsVideo);
    }

    [OrderedFact("Should add sticker to animated sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Add_Sticker_To_Animated_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.AnimatedThird
        );

        InputSticker inputSticker = new()
        {
            Sticker = InputFile.FromStream(stream, "Animated3.tgs"),
            EmojiList = classFixture.ThirdEmojis,
            Format = StickerFormat.Animated,
        };

        await BotClient.WithStreams(stream).AddStickerToSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestAnimatedRegularStickerSetName,
            sticker: inputSticker
        );

        await Task.Delay(1_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestAnimatedRegularStickerSetName
        );

        Assert.Equal(3, stickerSet.Stickers.Length);

        Sticker thirdSticker = stickerSet.Stickers[2];

        string thirdEmojisString = string.Concat(classFixture.ThirdEmojis);

        Assert.Equal(thirdEmojisString, thirdSticker.Emoji);
        Assert.True(thirdSticker.IsAnimated);
        Assert.False(thirdSticker.IsVideo);
    }

    [OrderedFact("Should add sticker to video sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Add_Sticker_To_Video_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.VideoThird
        );

        InputSticker inputSticker = new()
        {
            Sticker = InputFile.FromStream(stream, "Video3.webm"),
            EmojiList = classFixture.ThirdEmojis,
            Format = StickerFormat.Video,
        };

        await BotClient.WithStreams(stream).AddStickerToSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestVideoRegularStickerSetName,
            sticker: inputSticker
        );

        await Task.Delay(1_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestVideoRegularStickerSetName
        );

        Assert.Equal(3, stickerSet.Stickers.Length);

        Sticker thirdSticker = stickerSet.Stickers[2];

        string thirdEmojisString = string.Concat(classFixture.ThirdEmojis);

        Assert.Equal(thirdEmojisString, thirdSticker.Emoji);
        Assert.False(thirdSticker.IsAnimated);
        Assert.True(thirdSticker.IsVideo);
    }
    #endregion

    #region 5. Set sticker position in static set
    [OrderedFact("Should change sticker position in the test static regular sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerPositionInSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Change_Sticker_Position_In_Set()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker thirdSticker = stickerSet.Stickers[2];

        await BotClient.SetStickerPositionInSet(
            sticker: new(thirdSticker.FileId),
            position: 0
        );

        await Task.Delay(3_000);

        StickerSet positionedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker firstStickerInPositionedStickerSet = positionedStickerSet.Stickers.First();

        Assert.Equal(thirdSticker.FileUniqueId, firstStickerInPositionedStickerSet.FileUniqueId);
    }
    #endregion

    #region 6. Delete sticker from static set
    [OrderedFact("Should delete sticker from set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Delete_Sticker_From_Set()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker thirdSticker = stickerSet.Stickers[2];

        await BotClient.DeleteStickerFromSet(
            sticker: new(thirdSticker.FileId)
        );

        await Task.Delay(2_000);

        StickerSet updatedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.DoesNotContain(updatedStickerSet.Stickers, s => s.FileId == thirdSticker.FileId);
    }
    #endregion

    #region 7. Set static first sticker emoji list
    [OrderedFact("Should set first sticker emoji list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerEmojiList)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Set_First_Sticker_EmojiList()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker firstSticker = stickerSet.Stickers.First();

        string thirdEmojisString = string.Concat(classFixture.ThirdEmojis);

        Assert.Equal(thirdEmojisString, firstSticker.Emoji);

        await BotClient.SetStickerEmojiList(
            sticker: new(firstSticker.FileId),
            emojiList: classFixture.FirstEmojis
        );

        await Task.Delay(2_000);

        StickerSet updatedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker updatedFirstSticker = updatedStickerSet.Stickers.First();

        string firstEmojisString = string.Concat(classFixture.FirstEmojis);

        Assert.Equal(firstEmojisString, updatedFirstSticker.Emoji);
    }
    #endregion

    #region 8. Set static first sticker keywords
    [OrderedFact("Should set first sticker keywords")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerKeywords)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Set_First_Sticker_Keywords()
    {
        string[] keywords = ["test", "supertest"];

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker firstSticker = stickerSet.Stickers.First();

        await BotClient.SetStickerKeywords(
            sticker: new(firstSticker.FileId),
            keywords: keywords
        );

        await Task.Delay(1_000);

        await BotClient.SetStickerKeywords(
            sticker: new(firstSticker.FileId),
            keywords: null
        );
    }
    #endregion

    #region 9. Set static sticker set title
    [OrderedFact("Should set sticker set title")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerSetTitle)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Set_Sticker_Set_Title()
    {
        const string newStickerSetTitle = "New title for sticker set";

        await BotClient.SetStickerSetTitle(
            name: classFixture.TestStaticRegularStickerSetName,
            title: newStickerSetTitle
        );

        await Task.Delay(3_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.Equal(newStickerSetTitle, stickerSet.Title);
    }
    #endregion

    #region 10. Set static sticker set thumbnail
    [OrderedFact("Should set sticker set thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerSetThumbnail)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Set_Sticker_Set_Thumbnail()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.Regular.StaticThumbnail
        );

        await BotClient.WithStreams(stream).SetStickerSetThumbnail(
            name: classFixture.TestStaticRegularStickerSetName,
            userId: classFixture.OwnerUserId,
            thumbnail: InputFile.FromStream(stream),
            format: StickerFormat.Static
        );

        await Task.Delay(1_000);

        StickerSet updatedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Assert.NotNull(updatedStickerSet.Thumbnail);
        Assert.True(updatedStickerSet.Thumbnail.FileSize > 0);
    }
    #endregion

    #region 11. Some exceptions
    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to create sticker set with name not ending in _by_<bot username>")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    public async Task Should_Throw_InvalidStickerSetNameException()
    {
        const string expectedExceptionMessage = "Bad Request: invalid sticker set name is specified";

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromFileId(classFixture.TestUploadedStaticStickerFile.FileId),
                emojiList: classFixture.FirstEmojis,
                format: StickerFormat.Static
            )
        ];

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.CreateNewStickerSet(
                userId: classFixture.OwnerUserId,
                name: "Invalid_Sticker_Set_Name",
                title: classFixture.TestStickerSetTitle,
                stickers: inputStickers
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to create sticker with invalid emoji")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    public async Task Should_Throw_InvalidStickerEmojisException()
    {
        const string expectedExceptionMessage = "Bad Request: can't parse InputSticker: expected a Unicode emoji";

        string[] invalidEmojis = ["INVALID"];

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromFileId(classFixture.TestUploadedStaticStickerFile.FileId),
                emojiList: invalidEmojis,
                format: StickerFormat.Static
            )
        ];

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.CreateNewStickerSet(
                userId: classFixture.OwnerUserId,
                name: classFixture.TestStaticRegularStickerSetName,
                title: classFixture.TestStickerSetTitle,
                stickers: inputStickers
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to create sticker with invalid dimensions")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    public async Task Should_Throw_InvalidStickerDimensionsException()
    {
        // ToDo exception when sending jpeg file. Bad Request: STICKER_PNG_NOPNG

        const string expectedExceptionMessage = "Bad Request: STICKER_PNG_DIMENSIONS";

        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo);

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromStream(stream, "logo.png"),
                emojiList: classFixture.FirstEmojis,
                format: StickerFormat.Static
            )
        ];

        //New name, because an exception might be thrown: Bad Request: sticker set name is already occupied
        string newStickerSetName = $"new_{classFixture.TestStaticRegularStickerSetName}";

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.WithStreams(stream).CreateNewStickerSet(
                userId: classFixture.OwnerUserId,
                name: newStickerSetName,
                title: classFixture.TestStickerSetTitle,
                stickers: inputStickers
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to create sticker with invalid file size")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    public async Task Should_Throw_InvalidFileSizeException()
    {
        const string expectedExceptionMessage = "Bad Request: file is too big";

        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Apes);

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromStream(stream, "apes.jpg"),
                emojiList: classFixture.FirstEmojis,
                format: StickerFormat.Static
            )
        ];

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.WithStreams(stream).CreateNewStickerSet(
                userId: classFixture.OwnerUserId,
                name: classFixture.TestStaticRegularStickerSetName,
                title: classFixture.TestStickerSetTitle,
                stickers: inputStickers
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to create sticker set with the same name with ruby photo",
        Skip = "Bot API Bug")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    public async Task Should_Throw_StickerSetNameExistsException()
    {
        const string expectedExceptionMessage = "Bad Request: sticker set name is already occupied";

        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Ruby);

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromStream(stream, "ruby.png"),
                emojiList: classFixture.FirstEmojis,
                format: StickerFormat.Static
            )
        ];

        // Telegram for some reason does not return an error, so the test is skipped
        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.WithStreams(stream).CreateNewStickerSet(
                userId: classFixture.OwnerUserId,
                name: classFixture.TestStaticRegularStickerSetName,
                title: classFixture.TestStickerSetTitle,
                stickers: inputStickers
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [OrderedFact($"Should throw {nameof(ApiRequestException)} while trying to remove the last sticker in the set twice")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerFromSet)]
    public async Task Should_Throw_StickerSetNotModifiedException()
    {
        const string expectedExceptionMessage = "Bad Request: STICKERSET_NOT_MODIFIED";

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        Sticker lastSticker = stickerSet.Stickers.Last();

        await BotClient.DeleteStickerFromSet(
            sticker: new(lastSticker.FileId)
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await BotClient.DeleteStickerFromSet(
                sticker: new(lastSticker.FileId)
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
    #endregion

    #region 12. Delete sticker sets
    [OrderedFact("Should delete sticker sets")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Delete_Sticker_Sets()
    {
        const string expectedExceptionMessage = "Bad Request: STICKERSET_INVALID";

        await BotClient.DeleteStickerSet(
            name: classFixture.TestStaticRegularStickerSetName
        );

        await BotClient.DeleteStickerSet(
            name: classFixture.TestAnimatedRegularStickerSetName
        );

        await BotClient.DeleteStickerSet(
            name: classFixture.TestVideoRegularStickerSetName
        );

        await Task.Delay(2_500);

        ApiRequestException staticException = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.GetStickerSet(
                name: classFixture.TestStaticRegularStickerSetName
            )
        );

        ApiRequestException animatedException = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.GetStickerSet(
                name: classFixture.TestAnimatedRegularStickerSetName
            )
        );

        ApiRequestException videoException = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.GetStickerSet(
                name: classFixture.TestVideoRegularStickerSetName
            )
        );

        Assert.Equal(expectedExceptionMessage, staticException.Message);
        Assert.Equal(expectedExceptionMessage, animatedException.Message);
        Assert.Equal(expectedExceptionMessage, videoException.Message);
    }
    #endregion

    #region 13. Mask tests
    [OrderedFact("Should create new mask static sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Create_New_Mask_Static_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Photos.Tux
        );

        List<InputSticker> inputStickers = [
            new(
                sticker: InputFile.FromStream(stream, "tux.png"),
                emojiList: classFixture.SecondEmojis,
                format: StickerFormat.Static
            )
        ];

        await BotClient.WithStreams(stream).CreateNewStickerSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticMaskStickerSetName,
            title: classFixture.TestStickerSetTitle,
            stickers: inputStickers,
            stickerType: StickerType.Mask
        );

        await Task.Delay(1_000);

        classFixture.TestStaticMaskStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticMaskStickerSetName
        );

        Assert.Equal(StickerType.Mask, classFixture.TestStaticMaskStickerSet.StickerType);
        Assert.Single(classFixture.TestStaticMaskStickerSet.Stickers);
    }

    [OrderedFact("Should add VLC logo sticker with mask position like hat on forehead")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Add_Sticker_With_Mask_Position_To_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Vlc);

        InputSticker inputSticker = new(
            sticker: InputFile.FromStream(stream, "vlc.png"),
            emojiList: classFixture.SecondEmojis,
            format: StickerFormat.Static
        )
        {
            MaskPosition = new()
            {
                Point = MaskPositionPoint.Forehead,
                Scale = .8
            }
        };

        await BotClient.WithStreams(stream).AddStickerToSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticMaskStickerSetName,
            sticker: inputSticker
        );

        await Task.Delay(1_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticMaskStickerSetName
        );

        Assert.Equal(StickerType.Mask, stickerSet.StickerType);
        Assert.Equal(2, stickerSet.Stickers.Length);

        Sticker sticker = stickerSet.Stickers.Last();

        Assert.NotNull(sticker.MaskPosition);
        Assert.Equal(MaskPositionPoint.Forehead, sticker.MaskPosition.Point);
        Assert.Equal(.8, sticker.MaskPosition.Scale, .00001);
    }

    [OrderedFact("Should set mask position for first sticker")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    public async Task Should_Set_Mask_Position_From_Last_Sticker()
    {
        MaskPosition newMaskPosition = new()
        {
            Point = MaskPositionPoint.Chin,
            Scale = .42
        };

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticMaskStickerSetName
        );

        Sticker sticker = stickerSet.Stickers.First();

        await BotClient.SetStickerMaskPosition(
            sticker: new(sticker.FileId),
            maskPosition: newMaskPosition
        );

        await Task.Delay(1_000);

        StickerSet changedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticMaskStickerSetName
        );

        Sticker changedSticker = changedStickerSet.Stickers.First();

        Assert.NotNull(changedSticker.MaskPosition);
        Assert.Equal(newMaskPosition.Point, changedSticker.MaskPosition.Point);
        Assert.Equal(newMaskPosition.Scale, changedSticker.MaskPosition.Scale, .00001);
    }

    [OrderedFact("Should delete mask sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Delete_Mask_Sticker_Set()
    {
        const string expectedExceptionMessage = "Bad Request: STICKERSET_INVALID";

        await BotClient.DeleteStickerSet(
            name: classFixture.TestStaticMaskStickerSetName
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.GetStickerSet(
                name: classFixture.TestStaticMaskStickerSetName
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
    #endregion

    #region 14. Custom emoji tests
    [OrderedFact("Should create new custom emoji static sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Create_New_Custom_Emoji_Static_Sticker_Set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(
            Constants.PathToFile.Sticker.CustomEmoji.StaticFirst
        );

        List<InputSticker> inputStickers =
        [
            new(
                sticker: InputFile.FromStream(stream, "Static1.png"),
                emojiList: classFixture.FirstEmojis,
                format: StickerFormat.Static
            )
        ];

        await BotClient.WithStreams(stream).CreateNewStickerSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticCustomEmojiStickerSetName,
            title: classFixture.TestStickerSetTitle,
            stickers: inputStickers,
            stickerType: StickerType.CustomEmoji
        );

        await Task.Delay(1_000);

        classFixture.TestStaticCustomEmojiStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticCustomEmojiStickerSetName
        );

        Assert.Equal(StickerType.CustomEmoji, classFixture.TestStaticCustomEmojiStickerSet.StickerType);
        Assert.Single(classFixture.TestStaticCustomEmojiStickerSet.Stickers);
    }

    [OrderedFact("Should add sticker to a custom emoji set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Add_Sticker_To_A_Custom_Emoji_set()
    {
        await using System.IO.Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Sticker.CustomEmoji.StaticSecond);

        InputSticker inputSticker = new(
            sticker: InputFile.FromStream(stream, "Static2.png"),
            emojiList: classFixture.SecondEmojis,
            format: StickerFormat.Static
        );

        await BotClient.WithStreams(stream).AddStickerToSet(
            userId: classFixture.OwnerUserId,
            name: classFixture.TestStaticCustomEmojiStickerSetName,
            sticker: inputSticker
        );

        await Task.Delay(3_000);

        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticCustomEmojiStickerSetName
        );

        Assert.Equal(StickerType.CustomEmoji, stickerSet.StickerType);
        Assert.Equal(2, stickerSet.Stickers.Length);

        Sticker sticker = stickerSet.Stickers.Last();

        Assert.NotNull(sticker.CustomEmojiId);
    }

    [OrderedFact("Should set custom emoji set thumbnail")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetCustomEmojiStickerSetThumbnail)]
    public async Task Should_Set_Custom_Emoji_Set_Thumbnail()
    {
        StickerSet stickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticCustomEmojiStickerSetName
        );

        Sticker lastSticker = stickerSet.Stickers.Last();

        Assert.NotNull(lastSticker.CustomEmojiId);

        await BotClient.SetCustomEmojiStickerSetThumbnail(
            name: classFixture.TestStaticCustomEmojiStickerSetName,
            customEmojiId: lastSticker.CustomEmojiId
        );

        await Task.Delay(5_000);

        StickerSet changedStickerSet = await BotClient.GetStickerSet(
            name: classFixture.TestStaticCustomEmojiStickerSetName
        );

        Assert.NotNull(changedStickerSet.Thumbnail);
        Assert.NotNull(changedStickerSet.Thumbnail.FileSize);
        Assert.True(changedStickerSet.Thumbnail.FileSize > 0);
        Assert.Equal(100, changedStickerSet.Thumbnail.Width);
        Assert.Equal(100, changedStickerSet.Thumbnail.Height);
    }

    [OrderedFact("Should delete custom emoji sticker set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerSet)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
    public async Task Should_Delete_Custom_Emoji_Sticker_Set()
    {
        const string expectedExceptionMessage = "Bad Request: STICKERSET_INVALID";

        await BotClient.DeleteStickerSet(
            name: classFixture.TestStaticCustomEmojiStickerSetName
        );

        await Task.Delay(2_000);

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.GetStickerSet(
                name: classFixture.TestStaticCustomEmojiStickerSetName
            )
        );

        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
    #endregion
}
