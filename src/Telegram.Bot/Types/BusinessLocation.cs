// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Contains information about the location of a Telegram Business account.</summary>
public partial class BusinessLocation
{
    /// <summary>Address of the business</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary><em>Optional</em>. Location of the business</summary>
    public Location? Location { get; set; }
}
