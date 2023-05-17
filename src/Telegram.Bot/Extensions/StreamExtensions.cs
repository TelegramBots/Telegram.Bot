using System.IO;
using System.Runtime.CompilerServices;

namespace Telegram.Bot.Extensions;

internal static class StreamExtensions
{
    /// <summary>
    /// Deserialized JSON in Stream into <typeparamref name="T"/>
    /// </summary>
    /// <param name="stream"><see cref="Stream"/> with content</param>
    /// <typeparam name="T">Type of the resulting object</typeparam>
    /// <returns>Deserialized instance of <typeparamref name="T" /> or <c>null</c></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? DeserializeJsonFromStream<T>(this Stream? stream)
        where T : class
    {
        if (stream is null || !stream.CanRead) { return default; }

        using var streamReader = new StreamReader(stream);
        using var jsonTextReader = new JsonTextReader(streamReader);

        var jsonSerializer = JsonSerializer.CreateDefault();
        var searchResult = jsonSerializer.Deserialize<T>(jsonTextReader);

        return searchResult;
    }
}
