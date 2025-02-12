// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the message origin</summary>
[JsonConverter(typeof(EnumConverter<MessageOriginType>))]
public enum MessageOriginType
{
    /// <summary>The message was originally sent by a known user.<br/><br/><i>(<see cref="MessageOrigin"/> can be cast into <see cref="MessageOriginUser"/>)</i></summary>
    User = 1,
    /// <summary>The message was originally sent by an unknown user.<br/><br/><i>(<see cref="MessageOrigin"/> can be cast into <see cref="MessageOriginHiddenUser"/>)</i></summary>
    HiddenUser,
    /// <summary>The message was originally sent on behalf of a chat to a group chat.<br/><br/><i>(<see cref="MessageOrigin"/> can be cast into <see cref="MessageOriginChat"/>)</i></summary>
    Chat,
    /// <summary>The message was originally sent to a channel chat.<br/><br/><i>(<see cref="MessageOrigin"/> can be cast into <see cref="MessageOriginChannel"/>)</i></summary>
    Channel,
}
