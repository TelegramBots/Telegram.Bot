namespace Telegram.Bot.Types;

/// <summary>This object describes the type of a reaction. Currently, it can be one of<br/><see cref="ReactionTypeEmoji"/>, <see cref="ReactionTypeCustomEmoji"/></summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(ReactionTypeEmoji), "emoji")]
[CustomJsonDerivedType(typeof(ReactionTypeCustomEmoji), "custom_emoji")]
public abstract partial class ReactionType
{
    /// <summary>Type of the reaction</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ReactionTypeKind Type { get; }
}

/// <summary>The reaction is based on an emoji.</summary>
public partial class ReactionTypeEmoji : ReactionType
{
    /// <summary>Type of the reaction, always <see cref="ReactionTypeKind.Emoji"/></summary>
    public override ReactionTypeKind Type => ReactionTypeKind.Emoji;

    /// <summary>Reaction emoji. Currently, it can be one of "👍", "👎", "❤", "🔥", "🥰", "👏", "😁", "🤔", "🤯", "😱", "🤬", "😢", "🎉", "🤩", "🤮", "💩", "🙏", "👌", "🕊", "🤡", "🥱", "🥴", "😍", "🐳", "❤‍🔥", "🌚", "🌭", "💯", "🤣", "⚡", "🍌", "🏆", "💔", "🤨", "😐", "🍓", "🍾", "💋", "🖕", "😈", "😴", "😭", "🤓", "👻", "👨‍💻", "👀", "🎃", "🙈", "😇", "😨", "🤝", "✍", "🤗", "🫡", "🎅", "🎄", "☃", "💅", "🤪", "🗿", "🆒", "💘", "🙉", "🦄", "😘", "💊", "🙊", "😎", "👾", "🤷‍♂", "🤷", "🤷‍♀", "😡"</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Emoji { get; set; } = default!;
}

/// <summary>The reaction is based on a custom emoji.</summary>
public partial class ReactionTypeCustomEmoji : ReactionType
{
    /// <summary>Type of the reaction, always <see cref="ReactionTypeKind.CustomEmoji"/></summary>
    public override ReactionTypeKind Type => ReactionTypeKind.CustomEmoji;

    /// <summary>Custom emoji identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CustomEmojiId { get; set; } = default!;
}
