# Telegram.Bot.Extensions.Polling [![NuGet](https://img.shields.io/nuget/vpre/Telegram.Bot.Extensions.Polling.svg)](https://www.nuget.org/packages/Telegram.Bot.Extensions.Polling/) [![ci](https://github.com/TelegramBots/Telegram.Bot.Extensions.Polling/actions/workflows/ci.yml/badge.svg)](https://github.com/TelegramBots/Telegram.Bot.Extensions.Polling/actions/workflows/ci.yml)
[![Gitpod Ready-to-Code](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/from-referrer/)

Provides `ITelegramBotClient` extensions for polling updates.

## Usage

```csharp
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is Message message)
    {
        await botClient.SendTextMessageAsync(message.Chat, "Hello");
    }
}

async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    if (exception is ApiRequestException apiRequestException)
    {
        await botClient.SendTextMessageAsync(123, apiRequestException.ToString());
    }
}

ITelegramBotClient bot = new TelegramBotClient("<token>");
```

You have two ways of starting to receive updates
1. `StartReceiving` does not block the caller thread. Receiving is done on the ThreadPool.

    ```c#
    using System.Threading;
    using Telegram.Bot.Extensions.Polling;

    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = {} // receive all update types
    };
    bot.StartReceiving(
        HandleUpdateAsync,
        HandleErrorAsync,
        receiverOptions,
        cancellationToken
    );
    ```

2. Awaiting `ReceiveAsync` will block until cancellation in triggered (both methods accept a CancellationToken)

    ```c#
    using System.Threading;
    using Telegram.Bot.Extensions.Polling;

    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;

    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = {} // receive all update types
    };
   
   try
   {
        await bot.ReceiveAsync(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
   }
   catch (OperationCancelledException exception)
   {
   }
    ```

Trigger cancellation by calling `cts.Cancel()` somewhere to stop receiving update in both methods.

---

In case you want to throw out all pending updates on start there is an option
`ReceiveOptions.ThrowPendingUpdates`.
If set to `true` `ReceiveOptions.Offset` property will be ignored even if it's set to non-null value
and all implemented update receivers will attempt to throw out all pending updates before starting
to call your handlers. In that case `ReceiveOptions.AllowedUpdates` property should be set to
desired values otherwise it will be effectively set to allow all updates.

Example

```csharp
using System.Threading;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;

var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new { UpdateType.Message, UpdateType.CallbackQuery }
    ThrowPendingUpdates = true
};

try
{
    await bot.ReceiveAsync(
        HandleUpdateAsync,
        HandleErrorAsync,
        receiverOptions,
        cancellationToken
   );
}
catch (OperationCancelledException exception)
{
}
```

## Update streams

With .Net Core 3.1+ comes support for an `IAsyncEnumerable<Update>` to stream Updates as they are received.

There are two implementations:
- `BlockingUpdateReceiver` blocks execution on every new `getUpdates` request
- `QueuedUpdateReceiver` enqueues updates in an internal queue in a background process to make `Update` interation faster so you don't have to wait on `getUpdates` requests to finish

Example:

```csharp
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;

var bot = new TelegramBotClient("<token>");
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = {} // receive all update types
};
var updateReceiver = new QueuedUpdateReceiver(bot, receiverOptions);

// to cancel
var cts = new CancellationTokenSource();

try
{
    await foreach (Update update in updateReceiver.WithCancellation(cts.Token))
   {
       if (update.Message is Message message)
       {
           await bot.SendTextMessageAsync(
               message.Chat,
               $"Still have to process {updateReceiver.PendingUpdates} updates"
           );
       }
   }
}
catch (OperationCanceledException exception)
{
}
```

