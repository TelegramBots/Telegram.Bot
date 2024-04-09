namespace Telegram.Bot.Types;

/// <summary>
///
/// </summary>
public class BusinessIntro
{
    /// <summary>
    /// Optional. Title text of the business intro
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Optional. Message text of the business intro
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// Optional. Sticker of the business intro
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Sticker? Sticker { get; set; }
}
