namespace Telegram.Bot.Types;

/// <summary>Base class for file objects</summary>
public abstract partial class FileBase
{
    /// <summary>Identifier for this file, which can be used to download or reuse the file</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FileId { get; set; } = default!;

    /// <summary>Unique identifier for this file, which is supposed to be the same over time and for different bots. Can't be used to download or reuse the file.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FileUniqueId { get; set; } = default!;

    /// <summary><em>Optional</em>. File size in bytes.</summary>
    public long? FileSize { get; set; }
}
