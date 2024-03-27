namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// A marker interface for reply markups that define how a <see cref="User"/> can reply to the sent <see cref="Message"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = null)]
[JsonDerivedType(typeof(ForceReplyMarkup))]
[JsonDerivedType(typeof(ReplyKeyboardMarkup))]
[JsonDerivedType(typeof(InlineKeyboardMarkup))]
[JsonDerivedType(typeof(ReplyKeyboardRemove))]
public interface IReplyMarkup;
