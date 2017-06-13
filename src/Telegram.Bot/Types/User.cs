using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a Telegram user or bot.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        /// <summary>
        /// Unique identifier for this user or bot
        /// </summary>
        /// <returns></returns>
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public ChatId Id { get; set; }

        /// <summary>
        /// User's or bot's first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. User's or bot's last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name", Required = Required.Default)]
        public string LastName { get; set; }

        /// <summary>
        /// Optional. User's or bot's username
        /// </summary>
        [JsonProperty(PropertyName = "username", Required = Required.Default)]
        public string Username { get; internal set; }

        /// <summary>
        /// Returns if this User equals to another user
        /// </summary>
        /// <param name="obj">Another user</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is User)
                return (obj as User).Id == this.Id;
            else
                return false;
        }
        
        /// <summary>
        /// Returns if this User equals to another user
        /// </summary>
        /// <param name="obj">Another user</param>
        /// <returns></returns>
        public bool Equals(User user)
        {
            if (user != null)
                return (obj as User).Id == this.Id;
            
            return false;
        }
        
        public static bool operator ==(User a, User b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;
        
            return a.Id == b.Id;
        }
        
        public static bool operator !=(User a, User b)
        {
            return !(a == b);
        }
    }
}        
