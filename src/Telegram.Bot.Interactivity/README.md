# Telegram Interactivity Tools

## Requirements
* [Telegram.Bot library](https://github.com/TelegramBots/Telegram.Bot)
* .Net Core 2.2 or higher

## Setup
```csharp
// Create a bot client.
var botClient = new TelegramBotClient("YOURKEY");
botClient.UseInteractivity(new Interactivity.Types.InteractivityConfiguration()
{
    //Timeout time for an interactivity process.
    DefaultTimeOutTime = TimeSpan.FromMinutes(10)
});
//MANDATORY
botClient.StartReceiving();
```
## Usage
```csharp
private static async void OnMessageSent(object sender, MessageEventArgs e)
{
    if (e.Message.Text == "/hello")
    {
        //Ask the user for his name
        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Hello! What's your name?");
        // Get the client's interactivity object
        var interactivity = botClient.GetInteractivity();
        //Wait for a message.
        var result = await interactivity.WaitForMessageAsync(e.Message.Chat, e.Message.From);
        if (result.Value != null)
        {
            // The user responded in time.
            await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Hello, {result.Value}!");
        }
        else if (result.TimedOut)
        {
            // The user didn't respond within the set DefaultTimeOutTime, which is 2 minutes by default unless changed in the Configuration.
            await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"The process timed out.");
        }
        else
        {
            // An error has occured.
            await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"An unknown error has occured.");
        }
    }
}
```
#### TL;DR
To wait for a message, `await` the `WaitForMessageAsync()` function.

## Extras
### Conditions
You can set a condition for a message to be considered a valid response only if it is validated by passing it the `Predicate<Message>` parameter. Like so:

```csharp
// Get the client's interactivity.
var interactivity = botClient.GetInteractivity();
// Ask the user for their age.
await botClient.SendMessageAsync(chatId, 
    $"Hello! Please enter your age.");
// Wait for a message that can be parsed as an integer.
int age = 0;
await botClient
    .WaitForMessageAsync(
        e.Message.Chat,
        e.Message.From,
        sentMessage => int.TryParse(sentMessage.Text, out age));
Console.WriteLine($"The user is {age} years old!");
```