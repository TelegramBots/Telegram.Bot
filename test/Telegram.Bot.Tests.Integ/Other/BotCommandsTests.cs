using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.BotCommands)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class BotCommandsTests(TestsFixture fixture) : IAsyncLifetime
{
    BotCommandScope _scope;

    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should set a new bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    public async Task Should_Set_New_Bot_Command_List()
    {
        BotCommand[] commands =
        [
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            }
        ];

        _scope = BotCommandScope.Default();

        await BotClient.SetMyCommandsAsync(
            new SetMyCommandsRequest
            {
                Commands = commands,
                Scope = _scope,
            }
        );
    }

    [OrderedFact("Should get previously set bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetMyCommands)]
    public async Task Should_Get_Set_Bot_Commands()
    {
        BotCommand[] commands =
        [
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
        ];

        _scope = BotCommandScope.Default();

        await fixture.BotClient.SetMyCommandsAsync(
            new SetMyCommandsRequest
            {
                Commands = commands,
                Scope = _scope,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotCommand[] currentCommands = await fixture.BotClient.GetMyCommandsAsync(new GetMyCommandsRequest());

        Assert.Equal(2, currentCommands.Length);
        Asserts.JsonEquals(commands, currentCommands);
    }

    [OrderedFact("Should delete bot command list")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteMessage)]
    public async Task Should_Delete_Bot_Commands()
    {
        BotCommand[] commands =
        [
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            }
        ];

        _scope = BotCommandScope.Default();

        await BotClient.SetMyCommandsAsync(
            new SetMyCommandsRequest
            {
                Commands = commands,
                Scope = _scope,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotCommand[] setCommands = await BotClient.GetMyCommandsAsync(new GetMyCommandsRequest());

        Assert.NotNull(setCommands);
        Asserts.JsonEquals(commands, setCommands);

        await BotClient.DeleteMyCommandsAsync(new DeleteMyCommandsRequest { Scope = _scope});

        BotCommand[] currentCommands = await BotClient.GetMyCommandsAsync(new GetMyCommandsRequest { Scope = _scope });

        Assert.NotNull(currentCommands);
        Assert.Empty(currentCommands);
    }

    [OrderedFact("Should set group scoped commands")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetMyCommands)]
    public async Task Should_Set_Group_Scoped_Commands()
    {
        BotCommand[] commands =
        [
            new()
            {
                Command = "start",
                Description = "Start command"
            },
            new()
            {
                Command = "help",
                Description = "Help command"
            }
        ];

        _scope = BotCommandScope.AllGroupChats();

        await BotClient.SetMyCommandsAsync(
            new SetMyCommandsRequest
            {
                Commands = commands,
                Scope = _scope,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));

        BotCommand[] newCommands = await BotClient.GetMyCommandsAsync(new GetMyCommandsRequest { Scope = _scope });

        Asserts.JsonEquals(commands, newCommands);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() =>
        await fixture.BotClient.DeleteMyCommandsAsync(new DeleteMyCommandsRequest { Scope = _scope });
}
