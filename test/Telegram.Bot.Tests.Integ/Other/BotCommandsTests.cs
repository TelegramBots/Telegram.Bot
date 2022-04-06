using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotCommands)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotCommandsTests: IAsyncLifetime
{
    readonly TestsFixture _fixture;
    BotCommandScope _scope;

    ITelegramBotClient BotClient => _fixture.BotClient;

    public BotCommandsTests(TestsFixture fixture)
    {
        _fixture = fixture;
    }

    [OrderedFact("Should set a new bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    public async Task Should_Set_New_Bot_Command_List()
    {
        BotCommand[] commands =
        {
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            },
        };

        _scope = BotCommandScope.Default();

        await BotClient.SetMyCommandsAsync(
            commands: commands,
            scope: _scope
        );
    }

    [OrderedFact("Should get previously set bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyCommands)]
    public async Task Should_Get_Set_Bot_Commands()
    {
        BotCommand[] commands =
        {
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            },
        };

        _scope = BotCommandScope.Default();

        await _fixture.BotClient.SetMyCommandsAsync(
            commands: commands,
            scope: _scope
        );

        BotCommand[] currentCommands = await _fixture.BotClient.GetMyCommandsAsync();

        Assert.Equal(2, currentCommands.Length);
        Asserts.JsonEquals(commands, currentCommands);
    }

    [OrderedFact("Should delete bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteMessage)]
    public async Task Should_Delete_Bot_Commands()
    {
        BotCommand[] commands =
        {
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            },
        };

        _scope = BotCommandScope.Default();

        await BotClient.SetMyCommandsAsync(
            commands: commands,
            scope: _scope
        );

        BotCommand[] setCommands = await BotClient.GetMyCommandsAsync();

        Assert.NotNull(setCommands);
        Asserts.JsonEquals(commands, setCommands);

        await BotClient.DeleteMyCommandsAsync(scope: _scope);

        BotCommand[] currentCommands = await BotClient.GetMyCommandsAsync(scope: _scope);

        Assert.NotNull(currentCommands);
        Assert.Empty(currentCommands);
    }

    [OrderedFact("Should set group scoped commands")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    public async Task Should_Set_Group_Scoped_Commands()
    {
        BotCommand[] commands =
        {
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            },
        };

        _scope = BotCommandScope.AllGroupChats();

        await BotClient.SetMyCommandsAsync(
            commands: commands,
            scope: _scope
        );

        BotCommand[] newCommands = await BotClient.GetMyCommandsAsync(scope: _scope);

        Asserts.JsonEquals(commands, newCommands);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _fixture.BotClient.DeleteMyCommandsAsync(scope: _scope);
}