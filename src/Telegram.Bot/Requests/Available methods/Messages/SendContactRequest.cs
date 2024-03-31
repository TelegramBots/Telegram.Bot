using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send phone contacts. On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendContactRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <inheritdoc />
    public string? BusinessConnectionId { get; init; }

    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Contact's phone number
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Contact's first name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required string FirstName { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Contact's last name
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LastName { get; set; }

    /// <summary>
    /// Additional data about the contact in the form of a vCard, 0-2048 bytes
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Vcard { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyParameters"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [Obsolete($"This property is deprecated, use {nameof(ReplyParameters)} instead")]
    [JsonIgnore]
    public int? ReplyToMessageId
    {
        get => ReplyParameters?.MessageId;
        set
        {
            if (value is null)
            {
                ReplyParameters = null;
            }
            else
            {
                ReplyParameters ??= new();
                ReplyParameters.MessageId = value.Value;
            }
        }
    }

    /// <summary>
    /// Initializes a new request with chatId, phoneNumber and firstName
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SendContactRequest(ChatId chatId, string phoneNumber, string firstName)
        : this()
    {
        ChatId = chatId;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SendContactRequest()
        : base("sendContact")
    { }
}
