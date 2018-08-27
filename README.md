# Passport Extension for Telegram Bot API Client

This project is an extension to [Telegram.Bot] package for supporting [Telegram Passport] feature.

## Documentation

You can find documentation for this project in [Telegram Bots Book].

## Build

```bash
# ensure sub modules are updated
git submodule update --init --recursive

# use the feature branch for passport
cd deps/Telegram.Bot/
git checkout ext-passport
```

[Telegram.Bot]: https://github.com/TelegramBots/Telegram.Bot
[Telegram Passport]: https://telegram.org/blog/passport
[Telegram Bots Book]: https://telegrambots.github.io/book
