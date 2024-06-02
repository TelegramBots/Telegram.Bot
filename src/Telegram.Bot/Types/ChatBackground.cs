using JetBrains.Annotations;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a chat background
/// </summary>
[PublicAPI]
public class ChatBackground
{
    /// <summary>
    /// Type of the background
    /// </summary>
    public required BackgroundType Type { get; set; }
}
