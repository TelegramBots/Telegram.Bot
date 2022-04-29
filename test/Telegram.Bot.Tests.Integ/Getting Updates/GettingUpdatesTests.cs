using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Getting_Updates;

[Collection(Constants.TestCollections.GettingUpdates)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class GettingUpdatesTests
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    public GettingUpdatesTests(TestsFixture fixture) => _fixture = fixture;

    [OrderedFact("Should pass API Token test with valid token")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Pass_Test_Api_Token()
    {
        bool result = await BotClient.TestApiAsync();

        Assert.True(result);
    }

    [OrderedFact(
        "Should throw HttpRequestException with \"404 (Not Found)\" error when malformed API Token is provided"
    )]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Fail_Test_Api_Token()
    {
        ITelegramBotClient botClient = new TelegramBotClient(options: new("0:1this_is_an-invalid-token_for_tests"));

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            botClient.TestApiAsync()
        );

        Assert.IsType<ApiRequestException>(exception);
        Assert.Equal(404, exception.ErrorCode);
        Assert.Equal("Not Found", exception.Message);
    }

    [OrderedFact("Should fail API Token test with invalid token")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Test_Bad_BotToken()
    {
        ITelegramBotClient botClient = new TelegramBotClient(
            options: new("123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11")
        );
        bool result = await botClient.TestApiAsync();

        Assert.False(result);
    }

    [OrderedFact("Should get bot user info")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMe)]
    public async Task Should_Get_Bot_User()
    {
        User botUser = await BotClient.GetMeAsync();

        Assert.NotNull(botUser);
        Assert.NotNull(botUser.Username);
        Assert.True(botUser.IsBot);
        Assert.EndsWith("bot", botUser.Username!, StringComparison.OrdinalIgnoreCase);
    }
}
