using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Polling;

/// <summary>
/// Requests new <see cref="Update"/>s and processes them using provided <see cref="IUpdateHandler"/> instance
/// </summary>
[PublicAPI]
public interface IUpdateReceiver
{
    /// <summary>
    /// Starts receiving <see cref="Update"/>s invoking <see cref="IUpdateHandler.HandleUpdateAsync"/>
    /// for each <see cref="Update"/>.
    /// <para>This method will block if awaited.</para>
    /// </summary>
    /// <param name="updateHandler">
    /// The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> with which you can stop receiving
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that will be completed when cancellation will be requested through
    /// <paramref name="cancellationToken"/>
    /// </returns>
    Task ReceiveAsync(
        IUpdateHandler updateHandler,
        CancellationToken cancellationToken = default
    );
}
