using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// This object represents the content of a message to be sent as a result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
[CustomJsonPolymorphic]
[CustomJsonDerivedType<InputContactMessageContent>]
[CustomJsonDerivedType<InputInvoiceMessageContent>]
[CustomJsonDerivedType<InputLocationMessageContent>]
[CustomJsonDerivedType<InputTextMessageContent>]
[CustomJsonDerivedType<InputVenueMessageContent>]
public abstract class InputMessageContent;
