using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

[Collection(Constants.TestCollections.ChannelAdminBots)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ChannelAdminBotTests(TestsFixture fixture, ChannelAdminBotTestFixture classFixture)
    : TestClass(fixture), IClassFixture<ChannelAdminBotTestFixture>
{
    #region 1. Changing Chat Title

    [OrderedFact("Should set chat title")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
    public async Task Should_Set_Chat_Title()
    {
        await BotClient.SetChatTitle(
            chatId: classFixture.Chat.Id,
            title: "Test Chat Title"
        );
    }

    #endregion

    #region 2. Changing Chat Description

    [OrderedFact("Should set chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Set_Chat_Description()
    {
        await BotClient.SetChatDescription(
            chatId: classFixture.Chat.Id,
            description: "Test Chat Description"
        );
    }

    [OrderedFact("Should delete chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Delete_Chat_Description()
    {
        await BotClient.SetChatDescription(classFixture.Chat.Id);
    }

    #endregion

    #region 3. Pinning Chat Description

    [OrderedFact("Should pin chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
    public async Task Should_Pin_Message()
    {
        Message msg = await BotClient.SendMessage(
            chatId: classFixture.Chat.Id,
            text: "Description to pin"
        );

        await BotClient.PinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: msg.Id,
            disableNotification: true
        );

        classFixture.PinnedMessage = msg;
    }

    [OrderedFact("Should get chat’s pinned message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Chat_Pinned_Message()
    {
        Message pinnedMsg = classFixture.PinnedMessage;

        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

        Assert.NotNull(chat.PinnedMessage);
        Asserts.JsonEquals(pinnedMsg, chat.PinnedMessage);
    }

    [OrderedFact("Should unpin chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_Message()
    {
        await BotClient.UnpinChatMessage(classFixture.Chat.Id);
    }

    [OrderedFact("Should get the chat info without a pinned message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Chat_With_No_Pinned_Message()
    {
        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

        Assert.Null(chat.PinnedMessage);
    }

    #endregion

    #region 4. Changing Chat Photo

    [OrderedFact("Should set chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
    public async Task Should_Set_Chat_Photo()
    {
        await using Stream stream = File.OpenRead(Constants.PathToFile.Photos.Logo);
        await BotClient.WithStreams(stream).SetChatPhoto(
            chatId: classFixture.Chat.Id,
            photo: InputFile.FromStream(stream)
        );
    }

    [OrderedFact("Should get chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Chat_Photo()
    {
        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

        Assert.NotNull(chat.Photo);
        Assert.NotEmpty(chat.Photo.BigFileId);
        Assert.NotEmpty(chat.Photo.BigFileUniqueId);
        Assert.NotEmpty(chat.Photo.SmallFileId);
        Assert.NotEmpty(chat.Photo.SmallFileUniqueId);
    }

    [OrderedFact("Should delete chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Delete_Chat_Photo()
    {
        await BotClient.DeleteChatPhoto(classFixture.Chat.Id);
    }

    [OrderedFact("Should throw exception in deleting chat photo with no photo currently set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(
            async () => await BotClient.DeleteChatPhoto(classFixture.Chat.Id)
        );

        Assert.IsType<ApiRequestException>(e);
        Assert.Equal("Bad Request: CHAT_NOT_MODIFIED", e.Message);
    }

    #endregion

    #region 5. Chat Sticker Set

    [OrderedFact("Should throw exception when trying to set sticker set for a channel")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
    public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
    {
        const string setName = "EvilMinds";

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await Fixture.BotClient.SetChatStickerSet(classFixture.Chat.Id, setName)
        );

        Assert.Equal(400, exception.ErrorCode);
        Assert.Equal("Bad Request: method is available only for supergroups", exception.Message);
    }

    #endregion
}
