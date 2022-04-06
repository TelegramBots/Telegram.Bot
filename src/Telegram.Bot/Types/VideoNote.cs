﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a <see href="https://telegram.org/blog/video-messages-and-telescope">video message</see>
/// (available in Telegram apps as of
/// <see href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</see>).
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VideoNote : FileBase
{
    /// <summary>
    /// Video width and height (diameter of the video message) as defined by sender
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }

    /// <summary>
    /// Duration of the video in seconds as defined by sender
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Duration { get; set; }

    /// <summary>
    /// Optional. Video thumbnail
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize? Thumb { get; set; }
}