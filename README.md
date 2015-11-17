## Telegram Bot Api Library

C# library to talk to Telegrams Bot API (https://core.telegram.org/bots/api)

## Usage

```C#
static async void testApiAsync()
{
    var Bot = new Telegram.Bot.Api("your API access Token");
    var me = await Bot.GetMe();
    System.Console.WriteLine("Hello my name is " + me.FirstName);
}
```

```C#
static void testApi()
{
    var Bot = new Telegram.Bot.Api("your API access Token");
    var me = Bot.GetMe().Result;
    System.Console.WriteLine("Hello my name is " + me.FirstName);
}
```

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot/):

    Install-Package Telegram.Bot
    
## API Coverage

There are functions for all available API methods. (2015-11-17)
Missing: [Making requests when getting updates](https://core.telegram.org/bots/api#making-requests-when-getting-updates)
