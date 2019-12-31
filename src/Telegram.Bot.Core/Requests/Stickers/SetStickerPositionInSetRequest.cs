﻿// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Move a sticker in a set created by the bot to a specific position. Returns True on success.
    /// </summary>
    public class SetStickerPositionInSetRequest : RequestBase<bool>
    {
        /// <summary>
        /// File identifier of the sticker
        /// </summary>
        public string Sticker { get; }

        /// <summary>
        /// New sticker position in the set, zero-based
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initializes a new request with sticker and position
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        /// <param name="position">New sticker position in the set, zero-based</param>
        public SetStickerPositionInSetRequest(string sticker, int position)
            : base("setStickerPositionInSet")
        {
            Sticker = sticker;
            Position = position;
        }
    }
}
