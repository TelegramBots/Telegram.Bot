using System.ComponentModel;

using Newtonsoft.Json;

using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a <see cref="Game"/>.
    /// </summary>
    /// <seealso cref="InlineQueryResultNew" />
    public class InlineQueryResultGame : InlineQueryResultNew
    {
        /// <summary>
        /// Short name of the game.
        /// </summary>
        [JsonProperty("game_short_name", Required = Required.Always)]
        public string GameShortName { get; set; }

#pragma warning disable 1591
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Title { get; set; }

        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new InputMessageContent InputMessageContent { get; set; }

        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ThumbUrl { get; set; }

        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ThumbWidth { get; set; }

        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ThumbHeight { get; set; }
#pragma warning restore 1591
    }
}
