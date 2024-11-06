using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Getting_Updates;

[Collection(Constants.TestCollections.GettingUpdates)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GettingUpdatesTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should pass API Token test with valid token")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Pass_Test_Api_Token()
    {
        bool result = await BotClient.TestApi();

        Assert.True(result);
    }

    [OrderedFact(
        "Should throw HttpRequestException with \"404 (Not Found)\" error when malformed API Token is provided"
    )]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Fail_Test_Api_Token()
    {
        var botClient = Fixture.CreateClient(new() { ApiToken = "0:1this_is_an-invalid-token_for_tests",
            ClientApiToken = Fixture.Configuration.ClientApiToken });
        bool result = await botClient.TestApi();

        Assert.False(result);

        //ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
        //    botClient.TestApi()
        //);
        //Assert.IsType<ApiRequestException>(exception);
        //Assert.Equal(404, exception.ErrorCode);
        //Assert.Equal("Not Found", exception.Message);
    }

    [OrderedFact("Should fail API Token test with invalid token")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Test_Bad_BotToken()
    {
        var botClient = Fixture.CreateClient(new() { ApiToken = "123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11",
            ClientApiToken = Fixture.Configuration.ClientApiToken });
        bool result = await botClient.TestApi();

        Assert.False(result);
    }

    [OrderedFact("Should get bot user info")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Get_Bot_User()
    {
        User botUser = await BotClient.GetMe();

        Assert.NotNull(botUser);
        Assert.NotNull(botUser.Username);
        Assert.True(botUser.IsBot);
        Assert.EndsWith("bot", botUser.Username!, StringComparison.OrdinalIgnoreCase);
    }

    [OrderedFact("Should be aborted by global cancellation token")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Abort_Request_by_GlobalCancelToken()
    {
        CancellationTokenSource globalCT = new();
        var botClient = Fixture.CreateClient(Fixture.Configuration, globalCT.Token);
        globalCT.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await botClient.GetUpdates());
    }
}
