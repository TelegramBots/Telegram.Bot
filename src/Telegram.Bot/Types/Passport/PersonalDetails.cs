// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents personal details.</summary>
public partial class PersonalDetails : IDecryptedValue
{
    /// <summary>First Name</summary>
    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstName { get; set; } = default!;

    /// <summary>Last Name</summary>
    [JsonPropertyName("last_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string LastName { get; set; } = default!;

    /// <summary><em>Optional.</em> Middle Name</summary>
    [JsonPropertyName("middle_name")]
    public string? MiddleName { get; set; }

    /// <summary>Date of birth in DD.MM.YYYY format</summary>
    [JsonPropertyName("birth_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string BirthDate { get; set; } = default!;

    /// <summary>Gender, <em>male</em> or <em>female</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Gender { get; set; } = default!;

    /// <summary>Citizenship (ISO 3166-1 alpha-2 country code)</summary>
    [JsonPropertyName("country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CountryCode { get; set; } = default!;

    /// <summary>Country of residence (ISO 3166-1 alpha-2 country code)</summary>
    [JsonPropertyName("residence_country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ResidenceCountryCode { get; set; } = default!;

    /// <summary>First Name in the language of the user's country of residence</summary>
    [JsonPropertyName("first_name_native")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string FirstNameNative { get; set; } = default!;

    /// <summary>Last Name in the language of the user's country of residence</summary>
    [JsonPropertyName("last_name_native")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string LastNameNative { get; set; } = default!;

    /// <summary><em>Optional.</em> Middle Name in the language of the user's country of residence</summary>
    [JsonPropertyName("middle_name_native")]
    public string? MiddleNameNative { get; set; }
}
