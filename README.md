# .NET Client for Telegram Bot API

[![Telegram Chat](https://img.shields.io/badge/Chat-Telegram-blue.svg)](https://t.me/tgbots_dotnet)
[![license](https://img.shields.io/github/license/TelegramBots/telegram.bot.svg?maxAge=2592000)](https://raw.githubusercontent.com/TelegramBots/telegram.bot/master/LICENSE.txt)

|Package|Branch|Build|Test|
|:-----:|:----:|:---:|:--:|
| [NuGet ![NuGet Release](https://img.shields.io/nuget/vpre/Telegram.Bot.svg?label=Telegram.Bot&maxAge=3600)](https://www.nuget.org/packages/Telegram.Bot/) | `master` | [![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/master?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/master) | [![Test Status](https://img.shields.io/travis/TelegramBots/telegram.bot/master.svg?maxAge=3600&label=Test)](https://travis-ci.org/TelegramBots/telegram.bot) |
| [MyGet ![MyGet](https://img.shields.io/myget/telegram-bot/v/Telegram.bot.svg?label=Telegram.Bot&maxAge=3600)](https://www.myget.org/feed/telegram-bot/package/nuget/Telegram.Bot) | `develop` | [![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/develop?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/develop) | [![Test Status](https://img.shields.io/travis/TelegramBots/telegram.bot/develop.svg?maxAge=3600&label=Test)](https://travis-ci.org/TelegramBots/telegram.bot) |

.NET client for [Telegram Bot API](https://core.telegram.org/bots/api). The Bot API is an HTTP-based interface created for developers keen on building bots for Telegram.

Join our **super group on Telegram**: [`@tgbots_dotnet`](https://t.me/tgbots_dotnet)

> Our official product is [`Telegram.Bot`](https://www.nuget.org/packages/Telegram.Bot) NuGet package but if you need the latest features, use [MyGet feed](https://www.myget.org/feed/telegram-bot/package/nuget/Telegram.Bot) (auto deployed from `develop` branch) until we update the NuGet package.

## Getting Started

First, talk to [BotFather](https://t.me/botfather) on Telegram to get an API token. Place your token in method below and call the method.

```c#
static async Task TestApiAsync()
{
    var botClient = new Telegram.Bot.TelegramBotClient("your API access Token");
    var me = await botClient.GetMeAsync();
    System.Console.WriteLine("Hello! My name is " + me.FirstName);
}
```

## Learning More

If you don't know how to use this project or what is available for a Telegram bot, check the self-documented [Systems Integration tests](./test/Telegram.Bot.Tests.Integ/) which are runnable examples of API methods.

Before submitting issues please consult following resources:

* [Library docs](https://telegrambots.github.io/telegram.bot/)
* [Changelog](https://github.com/TelegramBots/telegram.bot/blob/master/CHANGELOG.md)
* [API docs](https://core.telegram.org/bots/api)
* [Webhook docs](https://core.telegram.org/bots/webhooks)
* [Examples](https://github.com/TelegramBots/telegram.bot.examples)
* [Tests cases](./test/Telegram.Bot.Tests.Integ/)

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot/):

Package manager:

```powershell
Install-Package Telegram.Bot
```

.NET CLI:

```bash
dotnet add package Telegram.Bot
```

For testing you can use the [MyGet feed](https://www.myget.org/gallery/telegram-bot) with automated builds.

## API Coverage

* [Bot API 3.4](https://core.telegram.org/bots/api-changelog)
* [Inline Mode](https://core.telegram.org/bots/inline)
* [Payments](https://core.telegram.org/bots/payments) (Needs some testing)
* [Games](https://core.telegram.org/bots/games)

Missing / TODO (last check 23.07.2017):

* [Making requests when getting updates](https://core.telegram.org/bots/api#making-requests-when-getting-updates)
