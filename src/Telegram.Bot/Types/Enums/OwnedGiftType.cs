// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the gift, always <see cref="OwnedGift"/></summary>
[JsonConverter(typeof(EnumConverter<OwnedGiftType>))]
public enum OwnedGiftType
{
    /// <summary>Describes a regular gift owned by a user or a chat.<br/><br/><i>(<see cref="OwnedGift"/> can be cast into <see cref="OwnedGiftRegular"/>)</i></summary>
    Regular = 1,
    /// <summary>Describes a unique gift received and owned by a user or a chat.<br/><br/><i>(<see cref="OwnedGift"/> can be cast into <see cref="OwnedGiftUnique"/>)</i></summary>
    Unique,
}
