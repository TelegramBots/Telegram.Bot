// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the profile photo</summary>
[JsonConverter(typeof(EnumConverter<InputProfilePhotoType>))]
public enum InputProfilePhotoType
{
    /// <summary>A static profile photo in the .JPG format.<br/><br/><i>(<see cref="InputProfilePhoto"/> can be cast into <see cref="InputProfilePhotoStatic"/>)</i></summary>
    Static = 1,
    /// <summary>An animated profile photo in the MPEG4 format.<br/><br/><i>(<see cref="InputProfilePhoto"/> can be cast into <see cref="InputProfilePhotoAnimated"/>)</i></summary>
    Animated,
}
