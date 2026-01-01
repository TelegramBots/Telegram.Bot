// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Returns the gifts owned and hosted by a user.<para>Returns: <see cref="OwnedGifts"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetUserGiftsRequest() : RequestBase<OwnedGifts>("getUserGifts"), IUserTargetable
{
    /// <summary>Unique identifier of the user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Pass <see langword="true"/> to exclude gifts that can be purchased an unlimited number of times</summary>
    [JsonPropertyName("exclude_unlimited")]
    public bool ExcludeUnlimited { get; set; }

    /// <summary>Pass <see langword="true"/> to exclude gifts that can be purchased a limited number of times and can be upgraded to unique</summary>
    [JsonPropertyName("exclude_limited_upgradable")]
    public bool ExcludeLimitedUpgradable { get; set; }

    /// <summary>Pass <see langword="true"/> to exclude gifts that can be purchased a limited number of times and can't be upgraded to unique</summary>
    [JsonPropertyName("exclude_limited_non_upgradable")]
    public bool ExcludeLimitedNonUpgradable { get; set; }

    /// <summary>Pass <see langword="true"/> to exclude gifts that were assigned from the TON blockchain and can't be resold or transferred in Telegram</summary>
    [JsonPropertyName("exclude_from_blockchain")]
    public bool ExcludeFromBlockchain { get; set; }

    /// <summary>Pass <see langword="true"/> to exclude unique gifts</summary>
    [JsonPropertyName("exclude_unique")]
    public bool ExcludeUnique { get; set; }

    /// <summary>Pass <see langword="true"/> to sort results by gift price instead of send date. Sorting is applied before pagination.</summary>
    [JsonPropertyName("sort_by_price")]
    public bool SortByPrice { get; set; }

    /// <summary>Offset of the first entry to return as received from the previous request; use an empty string to get the first chunk of results</summary>
    public string? Offset { get; set; }

    /// <summary>The maximum number of gifts to be returned; 1-100. Defaults to 100</summary>
    public int? Limit { get; set; }
}
