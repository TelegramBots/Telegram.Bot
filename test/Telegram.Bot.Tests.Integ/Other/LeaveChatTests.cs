using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.LeaveChat)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class LeaveChatTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should leave chat",
        Skip = "Bot should stay in chat for other the test cases")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.LeaveChat)]
    public async Task Should_Get_Private_Chat()
    {
        // ToDo: Exception when leaving private chat
        await BotClient.LeaveChatAsync(
            new LeaveChatRequest
            {
                ChatId = fixture.SupergroupChat,
            }
        );
    }
}
