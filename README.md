# Telegram Bot Api Library

[![NuGet Release](https://img.shields.io/nuget/vpre/Telegram.Bot.Core.svg?maxAge=3600)](https://www.nuget.org/packages/Telegram.Bot.Core)
[![Build Status](https://travis-ci.org/TelegramBots/Telegram.Bot.Core.svg?branch=master)](https://travis-ci.org/TelegramBots/Telegram.Bot.Core)
[![license](https://img.shields.io/github/license/TelegramBots/Telegram.Bot.Core.svg)](https://github.com/TelegramBots/Telegram.Bot.Core/blob/master/LICENSE)

C# library to talk to Telegrams [Bot API](https://core.telegram.org/bots/api)

## Basic Usage

```c#
static async Task TestApiAsync()
{
    var Bot = new Telegram.Bot.Api("your API access Token");
    var me = await Bot.GetMeAsync();
    System.Console.WriteLine("Hello my name is " + me.FirstName);
}
```

Before submitting issues please consult following resources:

* [Changelog](https://github.com/TelegramBots/Telegram.Bot.Core/blob/master/CHANGELOG.md)
* [API docs](https://core.telegram.org/bots/api)
* [Webook docs](https://core.telegram.org/bots/webhooks)
* Integration Tests
* [Examples](https://github.com/MrRoundRobin/telegram.bot.examples)

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot.Core):

```powershell
Install-Package Telegram.Bot.Core
```

## API Coverage

Up to [Bot API 3.1](https://core.telegram.org/bots/api-changelog#june-30-2017)

[Making requests when getting updates](https://core.telegram.org/bots/api#making-requests-when-getting-updates)
