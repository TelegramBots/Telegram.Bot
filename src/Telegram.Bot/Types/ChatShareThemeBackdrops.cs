// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains a list of backdrops, which can be used as the background of a theme of the screen for sharing a link to a user or a chat.</summary>
public partial class ChatShareThemeBackdrops
{
    /// <summary>The list of backdrops</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatShareThemeBackdrop[] Backdrops { get; set; } = default!;

    /// <summary>Implicit conversion to ChatShareThemeBackdrop[] (Backdrops)</summary>
    public static implicit operator ChatShareThemeBackdrop[](ChatShareThemeBackdrops self) => self.Backdrops;
    /// <summary>Implicit conversion from ChatShareThemeBackdrop[] (Backdrops)</summary>
    public static implicit operator ChatShareThemeBackdrops(ChatShareThemeBackdrop[] backdrops) => new() { Backdrops = backdrops };
}
