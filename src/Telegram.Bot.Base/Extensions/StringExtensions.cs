using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Telegram.Bot.Extensions;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string EncodeUtf8(this string value) =>
        new(Encoding.UTF8.GetBytes(value).Select(c => Convert.ToChar(c)).ToArray());
}
