using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages;

[Collection(Constants.TestCollections.SendTextMessage)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class TextMessageTests(TestsFixture fixture, TextMessageTests.ClassFixture classFixture)
    : TestClass(fixture), IClassFixture<TextMessageTests.ClassFixture>
{
    [OrderedFact("Should send text message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Text_Message()
    {
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "Hello world!"
        );

        Assert.Equal("Hello world!", message.Text);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.Equal(Fixture.SupergroupChat.Id.ToString(), message.Chat.Id.ToString());
        Assert.NotEqual(default, message.Date);
        Assert.NotNull(message.From);
        Assert.Equal(Fixture.BotUser.Id, message.From.Id);
        Assert.Equal(Fixture.BotUser.Username, message.From.Username);

        // getMe request returns more information than is present in received updates
        Asserts.UsersEqual(Fixture.BotUser, message.From);
    }

    [OrderedFact("Should send text message to channel")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Text_Message_To_Channel()
    {
        string text = $"Hello members of channel {classFixture.ChannelChatId}";

        Message message = await BotClient.SendMessage(
            chatId: classFixture.ChannelChatId,
            text: text
        );

        Assert.Equal(text, message.Text);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.Equal(classFixture.ChannelChat.Id, message.Chat.Id);
        Assert.Equal(classFixture.ChannelChat.Username, message.Chat.Username);
    }

    [OrderedFact("Should forward a message to same chat")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.ForwardMessage)]
    public async Task Should_Forward_Message()
    {
        Message message1 = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat,
            text: "➡️ Message to be forwared ⬅️"
        );

        Message message2 = await BotClient.ForwardMessage(
            chatId: Fixture.SupergroupChat,
            fromChatId: Fixture.SupergroupChat,
            messageId: message1.Id
        );

        MessageOriginUser forwardOrigin = (MessageOriginUser)message2.ForwardOrigin;
        Assert.NotNull(forwardOrigin);
        Asserts.UsersEqual(Fixture.BotUser, forwardOrigin.SenderUser);
        Assert.NotEqual(default, forwardOrigin.Date);
    }

    [OrderedFact("Should send markdown formatted text message and parse its entities. " +
                 "Link preview should not appear.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Parse_MarkDown_Entities()
    {
        const string url = "https://telegram.org/";
        Dictionary<MessageEntityType, string> entityValueMappings = new()
        {
            {MessageEntityType.Bold, "*bold*"},
            {MessageEntityType.Italic, "_italic_"},
            {MessageEntityType.TextLink, $"[inline url to Telegram.org]({url})"},
            {
                MessageEntityType.TextMention,
                $"[{Fixture.BotUser.GetSafeUsername()}](tg://user?id={Fixture.BotUser.Id})"
            },
            {MessageEntityType.Code, @"inline ""`fixed-width code`"""},
            {MessageEntityType.Pre, "```csharp\npre-formatted fixed-width code block```"},
        };

        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: string.Join("\n", entityValueMappings.Values),
            parseMode: ParseMode.Markdown,
            linkPreviewOptions: new() { IsDisabled = true }
        );

        Assert.NotNull(message.Entities);
        Assert.Equal(entityValueMappings.Keys, message.Entities.Select(e => e.Type));
        Assert.Equal(url, message.Entities.Single(e => e.Type == MessageEntityType.TextLink).Url);
        Asserts.UsersEqual(
            Fixture.BotUser,
            message.Entities.Single(e => e.Type == MessageEntityType.TextMention).User
        );
    }

    [OrderedFact("Should send HTML formatted text message and parse its entities. Link preview should not appear.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Parse_HTML_Entities()
    {
        const string url = "https://telegram.org/";
        (MessageEntityType Type, string Value)[] entityValueMappings =
        [
            (MessageEntityType.Bold, "<b>bold</b>"),
            (MessageEntityType.Bold, "<strong>&lt;strong&gt;</strong>"),
            (MessageEntityType.Italic, "<i>italic</i>"),
            (MessageEntityType.Italic, "<em>&lt;em&gt;</em>"),
            (MessageEntityType.TextLink, $@"<a href=""{url}"">inline url to Telegram.org</a>"),
            (
                MessageEntityType.TextMention,
                $@"<a href=""tg://user?id={Fixture.BotUser.Id}"">{Fixture.BotUser.Username}</a>"
            ),
            (MessageEntityType.Code, @"inline <code>""fixed-width code""</code>"),
            (MessageEntityType.Pre, "<pre><code class=\"language-csharp\">pre-formatted fixed-width code block</code></pre>"),
            (MessageEntityType.Strikethrough, "<s>strikethrough</s>"),
            (MessageEntityType.Underline, "<u>underline</u>"),
            (MessageEntityType.Spoiler, "<tg-spoiler>spoiler</tg-spoiler>"),
        ];

        var text = string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: text,
            parseMode: ParseMode.Html,
            linkPreviewOptions: new() { IsDisabled = true }
        );

        Assert.NotNull(message.Entities);
        Assert.Equal(
            entityValueMappings.Select(tuple => tuple.Type),
            message.Entities.Select(e => e.Type)
        );
        Assert.Equal(url, message.Entities.Single(e => e.Type == MessageEntityType.TextLink).Url);
        Asserts.UsersEqual(
            Fixture.BotUser,
            message.Entities.Single(e => e.Type == MessageEntityType.TextMention).User
        );
        Assert.Equal(text.Replace("strong>", "b>").Replace("em>", "i>"), message.ToHtml());
    }

    [OrderedFact("Should send text message and parse its entity values")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Parse_Message_Entities_Into_Values()
    {
        (MessageEntityType Type, string Value)[] entityValueMappings =
        [
            (MessageEntityType.PhoneNumber, "+38612345678"),
            (MessageEntityType.Cashtag, "$EUR"),
            (MessageEntityType.Hashtag, "#TelegramBots"),
            (MessageEntityType.Mention, "@BotFather"),
            (MessageEntityType.Url, "https://github.com/TelegramBots"),
            (MessageEntityType.Email, "security@telegram.org"),
            (MessageEntityType.BotCommand, "/test"),
            (MessageEntityType.BotCommand, $"/test@{Fixture.BotUser.Username}"),
        ];

        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))
        );

        Assert.NotNull(message.Entities);
        Assert.Equal(
            entityValueMappings.Select(t => t.Type),
            message.Entities.Select(e => e.Type)
        );
        Assert.Equal(entityValueMappings.Select(t => t.Value), message.EntityValues);
    }

    [OrderedFact("Should send MarkdownV2 formatted text message and parse its entities. " +
                 "Link preview should not appear.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Parse_MarkdownV2_Entities()
    {
        const string url = "https://telegram.org/";
        Dictionary<MessageEntityType, string> entityValueMappings = new()
        {
            {MessageEntityType.Bold, "*bold*"},
            {MessageEntityType.Italic, "_italic_"},
            {MessageEntityType.TextLink, $"[inline url to Telegram\\.org]({url})"},
            {
                MessageEntityType.TextMention,
                $"[{Fixture.BotUser.GetSafeUsername()}](tg://user?id={Fixture.BotUser.Id})"
            },
            {MessageEntityType.Code, @"inline ""`fixed-width code`"""},
            {MessageEntityType.Pre, "```csharp\npre-formatted fixed-width code block```"},
            {MessageEntityType.Strikethrough, "~strikethrough~"},
            {MessageEntityType.Underline, "__underline__"},
            {MessageEntityType.Spoiler, "||spoiler||"},
        };

        var text = string.Join("\n", entityValueMappings.Values);
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: text,
            parseMode: ParseMode.MarkdownV2,
            linkPreviewOptions: new() { IsDisabled = true }
        );

        Assert.NotNull(message.Entities);
        Assert.Equal(entityValueMappings.Keys, message.Entities.Select(e => e.Type));
        Assert.Equal(url, message.Entities.Single(e => e.Type == MessageEntityType.TextLink).Url);
        Asserts.UsersEqual(
            Fixture.BotUser,
            message.Entities.Single(e => e.Type == MessageEntityType.TextMention).User
        );
        Assert.Equal(text, message.ToMarkdown());
    }

    [OrderedFact("Should send text message with manual entities")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Manual_Entities()
    {
        const string url = "https://telegram.org/";
        string botUrl = $"tg://user?id={Fixture.BotUser.Id}";
        Dictionary<MessageEntityType, string> entityValueMappings = new()
        {
            {MessageEntityType.Bold, "bold"},
            {MessageEntityType.Italic, "italic"},
            {MessageEntityType.TextLink, $"inline url to Telegram.org"},
            {
                MessageEntityType.TextMention,
                Fixture.BotUser.GetSafeUsername()
            },
            {MessageEntityType.Code, @"inline ""fixed-width code"""},
            {MessageEntityType.Pre, "pre-formatted fixed-width code block"},
            {MessageEntityType.Strikethrough, "strikethrough"},
            {MessageEntityType.Underline, "underline"},
            {MessageEntityType.Spoiler, "spoiler"},
        };
        int offset = 0;
        IEnumerable<MessageEntity> entities = entityValueMappings.Select(kvp =>
        {
            MessageEntity entity = new() { Type = kvp.Key, Offset = offset, Length = kvp.Value.Length };
            if (entity.Type == MessageEntityType.TextLink) entity.Url = url;
            if (entity.Type == MessageEntityType.TextMention) entity.User = Fixture.BotUser;
            if (entity.Type == MessageEntityType.Pre) entity.Language = "csharp";
            offset += kvp.Value.Length + 1;
            return entity;
        }).ToList();
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: string.Join("\n", entityValueMappings.Values),
            entities: entities,
            parseMode: ParseMode.None,
            linkPreviewOptions: new() { IsDisabled = true }
        );

        Assert.NotNull(message.Entities);
        Assert.Equal(entityValueMappings.Keys, message.Entities.Select(e => e.Type));
        Assert.Equal(url, message.Entities.Single(e => e.Type == MessageEntityType.TextLink).Url);
        Asserts.UsersEqual(
            Fixture.BotUser,
            message.Entities.Single(e => e.Type == MessageEntityType.TextMention).User
        );
    }

    [OrderedFact("Should send a message with protected content")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Send_Message_With_Protected_Content()
    {
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "This content is protected!",
            protectContent: true
        );

        Assert.Equal("This content is protected!", message.Text);
        Assert.Equal(MessageType.Text, message.Type);
        Assert.Equal(Fixture.SupergroupChat.Id.ToString(), message.Chat.Id.ToString());
        Assert.NotEqual(default, message.Date);
        Assert.NotNull(message.From);
        Assert.Equal(Fixture.BotUser.Id, message.From.Id);
        Assert.Equal(Fixture.BotUser.Username, message.From.Username);
        Assert.True(message.HasProtectedContent);

        // getMe request returns more information than is present in received updates
        Asserts.UsersEqual(Fixture.BotUser, message.From);
    }

    [OrderedFact("Should throw when forwarding a protected message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    public async Task Should_Receive_Error_Trying_Forward_A_Message__With_Protected_Content()
    {
        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "This content is protected!",
            protectContent: true
        );

        Assert.True(message.HasProtectedContent);

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(
            async () => await BotClient.ForwardMessage(
                fromChatId: Fixture.SupergroupChat.Id,
                chatId: Fixture.SupergroupChat.Id,
                messageId: message.Id
            )
        );

        Assert.Equal(400, exception.ErrorCode);
    }

    public class ClassFixture(TestsFixture testsFixture)
        : ChannelChatFixture(testsFixture, Constants.TestCollections.SendTextMessage);
}
