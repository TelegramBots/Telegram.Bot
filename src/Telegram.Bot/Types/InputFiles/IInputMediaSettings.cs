// This file is NOT auto-generated
namespace Telegram.Bot.Types;

/// <summary>Indicates that an <see cref="InputMedia"/> has properties ShowCaptionAboveMedia and HasSpoiler.</summary>
public interface IInputMediaSettings
{
    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the caption must be shown above the message media</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the video needs to be covered with a spoiler animation</summary>
    public bool HasSpoiler { get; set; }
}
