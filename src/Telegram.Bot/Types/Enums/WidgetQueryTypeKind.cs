// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the query</summary>
[JsonConverter(typeof(EnumConverter<WidgetQueryTypeKind>))]
public enum WidgetQueryTypeKind
{
    /// <summary>The query contains data for the bot.<br/><br/><i>(<see cref="WidgetQueryType"/> can be cast into <see cref="WidgetQueryTypeData"/>)</i></summary>
    Data = 1,
    /// <summary>The query contains data signed by a TON wallet of the user who sent the query. The data is signed for the domain <c>bot&lt;BotId&gt;</c>, where <c>&lt;BotId&gt;</c> is the identifier of the bot.<br/><br/><i>(<see cref="WidgetQueryType"/> can be cast into <see cref="WidgetQueryTypeSignData"/>)</i></summary>
    SignData,
    /// <summary>The query notifies the bot about a transaction sent from a TON wallet of the user who sent the query.<br/><br/><i>(<see cref="WidgetQueryType"/> can be cast into <see cref="WidgetQueryTypeSentTransaction"/>)</i></summary>
    SentTransaction,
}
