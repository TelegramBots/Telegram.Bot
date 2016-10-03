using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a game. Use BotFather to create and edit games, their short names will act as unique identifiers.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Game
    {
        /// <summary>
        /// Title of the game.
        /// </summary>
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Description of the game.
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        /// <summary>
        /// Photo that will be displayed in the game message in chats.
        /// </summary>
        [JsonProperty("photo", Required = Required.Always)]
        public PhotoSize[] Photo { get; set; }

        /// <summary>
        /// Brief description of the game or high scores included in the game message.
        /// Can be automatically edited to include current high scores for the game when the bot calls setGameScore, or manually edited using editMessageText. 0-4096 characters.
        /// </summary>
        [JsonProperty("text", Required = Required.Default)]
        public string Text { get; set; }

        /// <summary>
        /// Special entities that appear in text, such as usernames, URLs, bot commands, etc.
        /// </summary>
        [JsonProperty("text_entities", Required = Required.Default)]
        public MessageEntity[] TextEntities { get; set; }

        /// <summary>
        /// Animation that will be displayed in the game message in chats. Upload via BotFather.
        /// </summary>
        [JsonProperty("animation", Required = Required.Default)]
        public Animation Animation { get; set; }
    }
}
