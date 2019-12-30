using System;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents a video to be sent
    /// </summary>
    public class InputMediaVideo : InputMediaBase, IInputMediaThumb, IAlbumInputMedia
    {
        /// <summary>
        /// Optional. Video width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Optional. Video height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Optional. Video duration
        /// </summary>
        public int Duration { get; set; }

        /// <inheritdoc />
        public InputMedia Thumb { get; set; }

        /// <summary>
        /// Optional. Pass True, if the uploaded video is suitable for streaming
        /// </summary>
        public bool SupportsStreaming { get; set; }

        /// <summary>
        /// Initializes a new video media to send
        /// </summary>
        [Obsolete("Use the other overload of this constructor with required parameter instead.")]
        public InputMediaVideo()
        {
            Type = "video";
        }

        /// <summary>
        /// Initializes a new video media to send with an <see cref="InputMedia"/>
        /// </summary>
        public InputMediaVideo(InputMedia media)
        {
            Type = "video";
            Media = media;
        }
    }
}
