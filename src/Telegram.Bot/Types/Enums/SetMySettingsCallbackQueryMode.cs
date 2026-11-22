// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>SetMySettings: Mode in which the bot receives <see cref="CallbackQuery">callback queries</see> from buttons in messages sent by the bot. Must be one of <see cref="Default">Default</see> (the field <em>message</em> is always used and contains an <a href="https://core.telegram.org/bots/api#inaccessiblemessage">InaccessibleMessage</a> if the message is inaccessible to the bot), <see cref="Auto">Auto</see> (the field <em>InlineMessageId</em> is used instead of the field <em>message</em> if the message is inaccessible to the bot; the field <em>message</em> can still contain an inaccessible message if the message has just been deleted), or <see cref="Inline">Inline</see> (the field <em>InlineMessageId</em> is always used instead of the field <em>message</em>).</summary>
[JsonConverter(typeof(EnumConverter<SetMySettingsCallbackQueryMode>))]
public enum SetMySettingsCallbackQueryMode
{
    /// <summary>“default” callbackquerymode</summary>
    Default = 1,
    /// <summary>“auto” callbackquerymode</summary>
    Auto,
    /// <summary>“inline” callbackquerymode</summary>
    Inline,
}
