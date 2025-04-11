// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the area</summary>
[JsonConverter(typeof(EnumConverter<StoryAreaTypeKind>))]
public enum StoryAreaTypeKind
{
    /// <summary>Describes a story area pointing to a location. Currently, a story can have up to 10 location areas.<br/><br/><i>(<see cref="StoryAreaType"/> can be cast into <see cref="StoryAreaTypeLocation"/>)</i></summary>
    Location = 1,
    /// <summary>Describes a story area pointing to a suggested reaction. Currently, a story can have up to 5 suggested reaction areas.<br/><br/><i>(<see cref="StoryAreaType"/> can be cast into <see cref="StoryAreaTypeSuggestedReaction"/>)</i></summary>
    SuggestedReaction,
    /// <summary>Describes a story area pointing to an HTTP or tg:// link. Currently, a story can have up to 3 link areas.<br/><br/><i>(<see cref="StoryAreaType"/> can be cast into <see cref="StoryAreaTypeLink"/>)</i></summary>
    Link,
    /// <summary>Describes a story area containing weather information. Currently, a story can have up to 3 weather areas.<br/><br/><i>(<see cref="StoryAreaType"/> can be cast into <see cref="StoryAreaTypeWeather"/>)</i></summary>
    Weather,
    /// <summary>Describes a story area pointing to a unique gift. Currently, a story can have at most 1 unique gift area.<br/><br/><i>(<see cref="StoryAreaType"/> can be cast into <see cref="StoryAreaTypeUniqueGift"/>)</i></summary>
    UniqueGift,
}
