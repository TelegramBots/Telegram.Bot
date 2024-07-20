# Contribution Guidelines

Any change to the code needs to be proven by passing the Unit or Systems Integration tests. Developers should add accompanying tests (of type either Unit or Systems Integration) to the set of changes in a PR. If a PR lacks the required tests, it should wait till all the tests are written before being merged into `develop` branch.

AppVeyor CI builds PRs and runs Unit tests. This step must be passed for PR before its merge.

Systems Integration tests(on Travi-CI) do not run on PRs and most likely a PR will be retargetted at another branch so maintainers can test the code before merging it into `develop` branch.

## PR Checklist

Before creating a pull request, make sure that your PR

- Has the HEAD commit from `develop` branch
- Has a clear message saying why this PR
- References/Explains any related issues

## Code Style

### Bot API Requests

All requests to Telegram Bot API are represented by classes derived from `RequestBase<TResult>`.

If a request class (and its accompanying method on `ITelegramBotClient`) accepts a collection, the type must be `IEnumerable<T>`. Also, return types of JSON array responses will be `TResult[]`.

## Related Documents

- [Systems Integration Tests - How To](https://github.com/TelegramBots/Telegram.Bot/blob/master/test/Telegram.Bot.Tests.Integ/README.md)
