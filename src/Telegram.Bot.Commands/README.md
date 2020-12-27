# Telegram Command Handler
A better way to detect and handle commands sent to a telegram client.

## Requirements
* [Telegram.Bot library](https://github.com/TelegramBots/Telegram.Bot)
* .Net Core 2.2 or higher

## Usage 
### Program.cs
Register your command classs(es) using `commandHandler.RegisterCommands<Class>` where `Class` is a subclass of `CommandModule`, then initialize the `commandHandler` with your `botClient`.
```csharp
TelegramCommandHandler commandHandler = new TelegramCommandHandler();
commandHandler.RegisterCommands<Commands>();
botClient.InitializeCommands(commandHandler);
//MANDATORY
botClient.StartReceiving();
```
### Commands.cs
```csharp
public class Commands : CommandModule
{

    // All command methods MUST be async, have a return type of Task, have only a CommandContext as a parameter,
    // and have the [Command] attribute.
    // The parameter for the [Command] attribute indicates what invokes this method. DO NOT specify a prefix here.
    // When a user invokes the /sayhello command, the bot will respond with "Hello, world!".
    [Command("sayhello")]
    public async Task SayHello(CommandContext ctx)
    {
        await ctx.RespondAsync("Hello, world!");
    }

    // When a user invokes the /ping command, the bot will respond with "Pong!".
    [Command("ping")]
    public async Task Ping(CommandContext ctx)
    {
        await ctx.RespondAsync("Pong!");
    }

}
```
## Extras
### Prefix
You can change the `/` prefix to anything you want by passing a paremeter to the `TelegramCommandHandler` initialization, like so:
```csharp
TelegramCommandHandler commandHandler = new TelegramCommandHandler("!");
```
This will cause `!` to invoke your commands, instead of `/`.
### Pre/Post-command execution
```csharp
public override async Task BeforeExecutionAsync(CommandContext ctx)
{
    // Will be called before execution of a method
}

public override async Task AfterExecutionAsync(CommandContext ctx)
{
    // Will be called after executionn of a method
}
```
