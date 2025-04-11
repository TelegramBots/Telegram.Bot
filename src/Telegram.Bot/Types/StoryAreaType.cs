// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the type of a clickable area on a story. Currently, it can be one of<br/><see cref="StoryAreaTypeLocation"/>, <see cref="StoryAreaTypeSuggestedReaction"/>, <see cref="StoryAreaTypeLink"/>, <see cref="StoryAreaTypeWeather"/>, <see cref="StoryAreaTypeUniqueGift"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<StoryAreaType>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(StoryAreaTypeLocation), "location")]
[CustomJsonDerivedType(typeof(StoryAreaTypeSuggestedReaction), "suggested_reaction")]
[CustomJsonDerivedType(typeof(StoryAreaTypeLink), "link")]
[CustomJsonDerivedType(typeof(StoryAreaTypeWeather), "weather")]
[CustomJsonDerivedType(typeof(StoryAreaTypeUniqueGift), "unique_gift")]
public abstract partial class StoryAreaType
{
    /// <summary>Type of the area</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract StoryAreaTypeKind Type { get; }
}

/// <summary>Describes a story area pointing to a location. Currently, a story can have up to 10 location areas.</summary>
public partial class StoryAreaTypeLocation : StoryAreaType
{
    /// <summary>Type of the area, always <see cref="StoryAreaTypeKind.Location"/></summary>
    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Location;

    /// <summary>Location latitude in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double Latitude { get; set; }

    /// <summary>Location longitude in degrees</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double Longitude { get; set; }

    /// <summary><em>Optional</em>. Address of the location</summary>
    public LocationAddress? Address { get; set; }
}

/// <summary>Describes a story area pointing to a suggested reaction. Currently, a story can have up to 5 suggested reaction areas.</summary>
public partial class StoryAreaTypeSuggestedReaction : StoryAreaType
{
    /// <summary>Type of the area, always <see cref="StoryAreaTypeKind.SuggestedReaction"/></summary>
    public override StoryAreaTypeKind Type => StoryAreaTypeKind.SuggestedReaction;

    /// <summary>Type of the reaction</summary>
    [JsonPropertyName("reaction_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ReactionType ReactionType { get; set; } = default!;

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the reaction area has a dark background</summary>
    [JsonPropertyName("is_dark")]
    public bool IsDark { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if reaction area corner is flipped</summary>
    [JsonPropertyName("is_flipped")]
    public bool IsFlipped { get; set; }
}

/// <summary>Describes a story area pointing to an HTTP or tg:// link. Currently, a story can have up to 3 link areas.</summary>
public partial class StoryAreaTypeLink : StoryAreaType
{
    /// <summary>Type of the area, always <see cref="StoryAreaTypeKind.Link"/></summary>
    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Link;

    /// <summary>HTTP or tg:// URL to be opened when the area is clicked</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;
}

/// <summary>Describes a story area containing weather information. Currently, a story can have up to 3 weather areas.</summary>
public partial class StoryAreaTypeWeather : StoryAreaType
{
    /// <summary>Type of the area, always <see cref="StoryAreaTypeKind.Weather"/></summary>
    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Weather;

    /// <summary>Temperature, in degree Celsius</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double Temperature { get; set; }

    /// <summary>Emoji representing the weather</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Emoji { get; set; } = default!;

    /// <summary>A color of the area background in the ARGB format</summary>
    [JsonPropertyName("background_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int BackgroundColor { get; set; }
}

/// <summary>Describes a story area pointing to a unique gift. Currently, a story can have at most 1 unique gift area.</summary>
public partial class StoryAreaTypeUniqueGift : StoryAreaType
{
    /// <summary>Type of the area, always <see cref="StoryAreaTypeKind.UniqueGift"/></summary>
    public override StoryAreaTypeKind Type => StoryAreaTypeKind.UniqueGift;

    /// <summary>Unique name of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;
}
