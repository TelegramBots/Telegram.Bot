using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Telegram.Bot.Extensions;

internal static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string EncodeUtf8(this string value) =>
        new(Encoding.UTF8.GetBytes(value).Select(c => Convert.ToChar(c)).ToArray());

    /// <summary>
    /// Converts a string to a correct <see cref="InputFile"/> implementation depending
    /// if it is a well formed URL or not
    /// </summary>
    /// <param name="urlOrFileId">A string with a URL of a file or a file id</param>
    /// <returns>A correct implementation of <see cref="InputFile"/></returns>
    public static InputFile ToInputFile(this string urlOrFileId) => InputFile.FromString(urlOrFileId);
}
