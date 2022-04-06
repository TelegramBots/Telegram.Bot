using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace Telegram.Bot;
#pragma warning disable CA1711
public delegate ValueTask AsyncEventHandler<in TArgs>(
#pragma warning restore CA1711
    ITelegramBotClient botClient,
    TArgs args,
    CancellationToken cancellationToken = default
);