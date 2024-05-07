using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Extensions;

public static class StreamExtensions
{
    /// <summary>
    /// Deserialized JSON in Stream into <typeparamref name="T"/>
    /// </summary>
    /// <param name="stream"><see cref="Stream"/> with content</param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">Type of the resulting object</typeparam>
    /// <returns>Deserialized instance of <typeparamref name="T" /> or <c>null</c></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<T?> DeserializeJsonFromStreamAsync<T>(
        this Stream? stream,
        JsonTypeInfo<T> context,
        CancellationToken cancellationToken = default)
        where T : class
    {
        if (stream is null || !stream.CanRead) { return default; }

        return await JsonSerializer
            .DeserializeAsync(
                utf8Json: stream,
                context,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);
    }
}
