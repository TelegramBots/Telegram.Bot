using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// This object represents the content of a message to be sent as a result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
[CustomJsonPolymorphic]
[CustomJsonDerivedType(typeof(InputContactMessageContent))]
[CustomJsonDerivedType(typeof(InputInvoiceMessageContent))]
[CustomJsonDerivedType(typeof(InputLocationMessageContent))]
[CustomJsonDerivedType(typeof(InputTextMessageContent))]
[CustomJsonDerivedType(typeof(InputVenueMessageContent))]
public abstract class InputMessageContent;
