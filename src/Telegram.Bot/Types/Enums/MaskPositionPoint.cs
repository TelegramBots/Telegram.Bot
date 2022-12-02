namespace Telegram.Bot.Types.Enums;

/// <summary>
/// The part of the face relative to which the mask should be placed.
/// </summary>
[JsonConverter(typeof(MaskPositionPointConverter))]
public enum MaskPositionPoint
{
    /// <summary>
    /// The forehead
    /// </summary>
    Forehead = 1,

    /// <summary>
    /// The eyes
    /// </summary>
    Eyes,

    /// <summary>
    /// The mouth
    /// </summary>
    Mouth,

    /// <summary>
    /// The chin
    /// </summary>
    Chin
}
