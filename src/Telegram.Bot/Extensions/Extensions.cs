using System;
using System.Runtime.CompilerServices;

namespace Telegram.Bot.Extensions;

/// <summary>
/// Extension Methods
/// </summary>
internal static class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T ThrowIfNull<T>(this T? value, string parameterName) =>
        value ?? throw new ArgumentNullException(parameterName);
}
