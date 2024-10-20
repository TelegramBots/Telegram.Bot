using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages;

[Collection(Constants.TestCollections.DeleteMessage)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class DeleteMessageTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should delete message generated from an inline query result")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    public async Task Should_Delete_Message_From_InlineQuery()
    {
        await Fixture.SendTestInstructionsAsync(
            "Starting the inline query with this message...",
            startInlineQuery: true
        );

        Update queryUpdate = await Fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

        await BotClient.AnswerInlineQuery(
            inlineQueryId: queryUpdate.InlineQuery!.Id,
            results:
            [
                new InlineQueryResultArticle
                {
                    Id = "article-to-delete",
                    Title = "Telegram Bot API",
                    InputMessageContent = new InputTextMessageContent { MessageText = "https://www.telegram.org/"},
                }
            ],
            cacheTime: 0
        );

        (Update messageUpdate, _) =
            await Fixture.UpdateReceiver.GetInlineQueryResultUpdates(
                chatId: Fixture.SupergroupChat.Id,
                messageType: MessageType.Text
            );

        await Task.Delay(1_000);

        await BotClient.DeleteMessage(
            chatId: messageUpdate.Message!.Chat.Id,
            messageId: messageUpdate.Message.Id
        );
    }
}
