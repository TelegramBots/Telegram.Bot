using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// The part of the face relative to which the mask should be placed. One of “forehead”, “eyes”, “mouth”, or “chin”.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MaskPositionPoint
    {
        /// <summary>
        /// The forehead
        /// </summary>
        [EnumMember(Value = "forehead")]
        Forehead,
        /// <summary>
        /// The eyes
        /// </summary>
        [EnumMember(Value = "eyes")]
        Eyes,
        /// <summary>
        /// The mouth
        /// </summary>
        [EnumMember(Value = "mouth")]
        Mouth,
        /// <summary>
        /// The chin
        /// </summary>
        [EnumMember(Value = "chin")]
        Chin
    }
}
