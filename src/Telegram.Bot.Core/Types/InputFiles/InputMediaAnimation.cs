

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent
    /// </summary>
    public class InputMediaAnimation : InputMediaBase, IInputMediaThumb
    {
        /// <summary>
        /// Optional. Animation width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Optional. Animation height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Optional. Animation duration
        /// </summary>
        public int Duration { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <summary>
        /// Initializes a new animation media to send with an <see cref="InputMedia"/>
        /// </summary>
        public InputMediaAnimation(InputMedia media)
        {
            Type = "animation";
            Media = media;
        }
    }
}
