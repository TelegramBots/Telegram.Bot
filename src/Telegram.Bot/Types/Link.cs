// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents an HTTP link.</summary>
public partial class Link
{
    /// <summary>URL of the link</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;

    /// <summary>Implicit conversion to string (Url)</summary>
    public static implicit operator string(Link self) => self.Url;
    /// <summary>Implicit conversion from string (Url)</summary>
    public static implicit operator Link(string url) => new() { Url = url };
}
