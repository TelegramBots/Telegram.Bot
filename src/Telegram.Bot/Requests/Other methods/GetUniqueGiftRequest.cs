// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about a unique gift by its name.<para>Returns: A <see cref="UniqueGift"/> object on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetUniqueGiftRequest() : RequestBase<UniqueGift>("getUniqueGift")
{
    /// <summary>Unique name of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Name { get; set; }
}
