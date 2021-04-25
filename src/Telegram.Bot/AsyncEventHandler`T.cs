using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public delegate ValueTask AsyncEventHandler<in TArgs>(
        ITelegramBotClient botClient,
        TArgs args,
        CancellationToken cancellationToken = default
    );
}
