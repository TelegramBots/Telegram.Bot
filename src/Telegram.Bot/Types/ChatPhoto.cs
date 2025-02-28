// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a chat photo.</summary>
public partial class ChatPhoto
{
    /// <summary>File identifier of small (160x160) chat photo. This FileId can be used only for photo download and only for as long as the photo is not changed.</summary>
    [JsonPropertyName("small_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SmallFileId { get; set; } = default!;

    /// <summary>Unique file identifier of small (160x160) chat photo, which is supposed to be the same over time and for different bots. Can't be used to download or reuse the file.</summary>
    [JsonPropertyName("small_file_unique_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SmallFileUniqueId { get; set; } = default!;

    /// <summary>File identifier of big (640x640) chat photo. This FileId can be used only for photo download and only for as long as the photo is not changed.</summary>
    [JsonPropertyName("big_file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BigFileId { get; set; } = default!;

    /// <summary>Unique file identifier of big (640x640) chat photo, which is supposed to be the same over time and for different bots. Can't be used to download or reuse the file.</summary>
    [JsonPropertyName("big_file_unique_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BigFileUniqueId { get; set; } = default!;
}
