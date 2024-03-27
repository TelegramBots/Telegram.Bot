using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the type of a reaction. Currently, it can be one of
/// <list type="bullet">
/// <item><see cref="ReactionTypeEmoji"/></item>
/// <item><see cref="ReactionTypeCustomEmoji"/></item>
/// </list>
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType<ReactionTypeEmoji>("emoji")]
[CustomJsonDerivedType<ReactionTypeCustomEmoji>("custom_emoji")]
public abstract class ReactionType
{
    /// <summary>
    /// Type of the reaction
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ReactionTypeKind Type { get; }
}

/// <summary>
/// The reaction is based on an emoji.
/// </summary>
public class ReactionTypeEmoji : ReactionType
{
    /// <summary>
    /// Type of the reaction, always "emoji"
    /// </summary>
    public override ReactionTypeKind Type => ReactionTypeKind.Emoji;

    /// <summary>
    /// Reaction emoji. Currently, it can be one of "👍", "👎", "❤", "🔥", "🥰", "👏", "😁",
    /// "🤔", "🤯", "😱", "🤬", "😢", "🎉", "🤩", "🤮", "💩", "🙏", "👌", "🕊", "🤡", "🥱",
    /// "🥴", "😍", "🐳", "❤‍🔥", "🌚", "🌭", "💯", "🤣", "⚡", "🍌", "🏆", "💔", "🤨",
    /// "😐", "🍓", "🍾", "💋", "🖕", "😈", "😴", "😭", "🤓", "👻", "👨‍💻", "👀", "🎃",
    /// "🙈", "😇", "😨", "🤝", "✍", "🤗", "🫡", "🎅", "🎄", "☃", "💅", "🤪", "🗿", "🆒",
    /// "💘", "🙉", "🦄", "😘", "💊", "🙊", "😎", "👾", "🤷‍♂", "🤷", "🤷‍♀", "😡"
    /// </summary>
    /// <remarks>
    /// Available shortcuts: <see cref="Enums.KnownReactionTypeEmoji"/>
    /// </remarks>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Emoji { get; set; } = default!;
}

/// <summary>
/// The reaction is based on an emoji.
/// </summary>
public class ReactionTypeCustomEmoji : ReactionType
{
    /// <summary>
    /// Type of the reaction, always "custom_emoji"
    /// </summary>
    public override ReactionTypeKind Type => ReactionTypeKind.CustomEmoji;

    /// <summary>
    /// Custom emoji identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CustomEmojiId { get; set; } = default!;
}
