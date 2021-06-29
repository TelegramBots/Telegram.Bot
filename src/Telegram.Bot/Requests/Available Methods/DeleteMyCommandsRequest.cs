using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Delete the list of the bot's commands for the given <see cref="DeleteMyCommandsRequest.Scope"/> and
    /// user language. After deletion, higher level commands will be shown to affected users.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class DeleteMyCommandsRequest : RequestBase<bool>
    {
        /// <summary>
        /// An object, describing scope of users for which the commands are relevant.
        /// Defaults to <see cref="BotCommandScopeDefault"/>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BotCommandScope Scope { get; set; }

        /// <summary>
        /// A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given
        /// <see cref="GetMyCommandsRequest.Scope"/>, for whose language there are no dedicated commands
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public DeleteMyCommandsRequest()
            : base("deleteMyCommands")
        { }
    }
}
