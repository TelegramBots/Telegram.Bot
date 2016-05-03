[![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/master?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/master)

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

see [telegram.bot.examples](https://github.com/MrRoundRobin/telegram.bot.examples)

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot/):

```
Install-Package Telegram.Bot
```

## API Coverage

Updated to [Bot API 2.0](https://core.telegram.org/bots/2-0-intro)

Missing / TODO (last check 28.04.2016):
  * [Making requests when getting updates](https://core.telegram.org/bots/api#making-requests-when-getting-updates)