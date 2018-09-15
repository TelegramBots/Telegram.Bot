# Telegram Passport

[![package](https://img.shields.io/nuget/v/Telegram.Bot.Extensions.Passport.svg?style=flat-square)](https://www.nuget.org/packages/Telegram.Bot.Extensions.Passport)
[![build](https://img.shields.io/appveyor/tests/poulad/telegram-bot-extensions-passport/master.svg?style=flat-square)](https://ci.appveyor.com/project/poulad/telegram-bot-extensions-passport)
[![documentations](https://img.shields.io/badge/docs-book-orange.svg?style=flat-square)](https://telegrambots.github.io/book/4/passport)

This project is an extension to [Telegram.Bot] package for supporting [Telegram Passport] feature.
You need to add `Telegram.Bot.Extensions.Passport` extension package to your project
in addition to the core package (`Telegram.Bot`).

```bash
dotnet add package Telegram.Bot.Extensions.Passport
```

## 📖 Documentation

You can find documentation for this project including the quickstart guide in [Telegram Bots Book].

## 🚧 Supported Platforms

Project targets **.NET Standard 2.0** and **.NET Framework 4.6.1** at minimum.

## 🔨 Build & Contribute 👋

```bash
# ensure sub modules are updated
git submodule update --init --recursive

# use the feature branch for passport
cd deps/Telegram.Bot/
git checkout ext-passport
```

[Telegram.Bot]: https://github.com/TelegramBots/Telegram.Bot
[Telegram Passport]: https://telegram.org/blog/passport
[Telegram Bots Book]: https://telegrambots.github.io/book/4/passport
