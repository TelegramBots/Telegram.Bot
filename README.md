# Telegram Bot Api Library

[![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/master?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/master)
[![NuGet Release](https://img.shields.io/nuget/vpre/Telegram.Bot.svg?maxAge=3600)](https://www.nuget.org/packages/Telegram.Bot/)
[![MyGet](https://img.shields.io/myget/telegram-bot/v/Telegram.bot.svg?maxAge=3600)](https://www.myget.org/feed/telegram-bot/package/nuget/Telegram.Bot)
[![license](https://img.shields.io/github/license/mrroundrobin/telegram.bot.svg?maxAge=2592000)](https://raw.githubusercontent.com/MrRoundRobin/telegram.bot/master/LICENSE.txt)

C# library to talk to Telegrams [Bot API](https://core.telegram.org/bots/api)

## Basic Usage

```C#
static async void testApiAsync()
{
    var Bot = new Telegram.Bot.Api("your API access Token");
    var me = await Bot.GetMeAsync();
    System.Console.WriteLine("Hello my name is " + me.FirstName);
}
```

Before submitting issues please consult following resources:

* [Library docs](https://mrroundrobin.github.io/telegram.bot/)
* [Changelog](https://github.com/MrRoundRobin/telegram.bot/blob/master/CHANGELOG.md)
* [API docs](https://core.telegram.org/bots/api)
* [Webhook docs](https://core.telegram.org/bots/webhooks)
* [Examples](https://github.com/MrRoundRobin/telegram.bot.examples)

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot/):

```powershell
Install-Package Telegram.Bot
```

For testing you can use the [MyGet feed](https://www.myget.org/gallery/telegram-bot) with automated builds

## API Coverage

* [Inline Mode](https://core.telegram.org/bots/inline)
* [Bot API 3.1](https://core.telegram.org/bots/api-changelog)
* [Payments](https://core.telegram.org/bots/payments) (Needs some testing)
* [Games](https://core.telegram.org/bots/games)

Missing / TODO (last check 13.07.2017):

* [Making requests when getting updates](https://core.telegram.org/bots/api#making-requests-when-getting-updates)
