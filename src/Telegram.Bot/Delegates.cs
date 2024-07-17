using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace Telegram.Bot;

public delegate ValueTask AsyncEventHandler<in TArgs>(ITelegramBotClient botClient, TArgs args, CancellationToken cancellationToken = default);
public delegate Task OnUpdateHandler(Update update);
public delegate Task OnMessageHandler(Message message, UpdateType type);
public delegate Task OnErrorHandler(Exception exception, Polling.HandleErrorSource source);

