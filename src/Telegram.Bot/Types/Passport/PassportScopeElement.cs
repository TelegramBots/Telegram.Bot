// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>This object represents a requested element, should be one of:<br/><see cref="PassportScopeElementOneOfSeveral"/>, <see cref="PassportScopeElementOne"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<PassportScopeElement>))]
[CustomJsonPolymorphic()]
[CustomJsonDerivedType(typeof(PassportScopeElementOneOfSeveral))]
[CustomJsonDerivedType(typeof(PassportScopeElementOne))]
public abstract partial class PassportScopeElement;

/// <summary>This object represents several elements one of which must be provided.</summary>
public partial class PassportScopeElementOneOfSeveral : PassportScopeElement
{
    /// <summary>List of elements one of which must be provided; must contain either several of “passport”, “DriverLicense”, “IdentityCard”, “InternalPassport” <b>or</b> several of “UtilityBill”, “BankStatement”, “RentalAgreement”, “PassportRegistration”, “TemporaryRegistration”</summary>
    [JsonPropertyName("one_of")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PassportScopeElementOne[] OneOf { get; set; } = default!;

    /// <summary><em>Optional.</em> Use this parameter if you want to request a selfie with the document from this list that the user chooses to upload.</summary>
    public bool Selfie { get; set; }

    /// <summary><em>Optional.</em> Use this parameter if you want to request a translation of the document from this list that the user chooses to upload. <b>Note:</b> We suggest to only request translations <em>after</em> you have received a valid document that requires one.</summary>
    public bool Translation { get; set; }
}

/// <summary>This object represents one particular element that must be provided. If no options are needed, <em>String</em> can be used instead of this object to specify the type of the element.<br/>You can also use the special type "IdDocument" as an alias for one of "passport", "DriverLicense", "IdentityCard" and the special type "AddressDocument" as an alias for one of "UtilityBill", "BankStatement", "RentalAgreement". So <c>{"type":"IdDocument",selfie:true}</c> is equal to <c>{"OneOf":["passport","DriverLicense","IdentityCard"],selfie:true}</c>.</summary>
public partial class PassportScopeElementOne : PassportScopeElement
{
    /// <summary>Element type. One of "PersonalDetails", "passport", "DriverLicense", "IdentityCard", "InternalPassport", "address", "UtilityBill", "BankStatement", "RentalAgreement", "PassportRegistration", "TemporaryRegistration", "PhoneNumber", "email"</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public EncryptedPassportElementType Type { get; set; }

    /// <summary><em>Optional.</em> Use this parameter if you want to request a selfie with the document as well. Available for "passport", "DriverLicense", "IdentityCard" and "InternalPassport"</summary>
    public bool Selfie { get; set; }

    /// <summary><em>Optional.</em> Use this parameter if you want to request a translation of the document as well. Available for "passport", "DriverLicense", "IdentityCard", "InternalPassport", "UtilityBill", "BankStatement", "RentalAgreement", "PassportRegistration" and "TemporaryRegistration". <b>Note:</b> We suggest to only request translations <em>after</em> you have received a valid document that requires one.</summary>
    public bool Translation { get; set; }

    /// <summary><em>Optional.</em> Use this parameter to request the first, last and middle name of the user in the language of the user's country of residence. Available for "PersonalDetails"</summary>
    [JsonPropertyName("native_names")]
    public bool NativeNames { get; set; }
}
