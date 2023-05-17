﻿using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// Represents a join request sent to a chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatJoinRequest
{
    /// <summary>
    /// Chat to which the request was sent
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// User that sent the join request
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Identifier of a private chat with the user who sent the join request. This number may have more than 32
    /// significant bits and some programming languages may have difficulty/silent defects in interpreting it. But it
    /// has at most 52 significant bits, so a 64-bit integer or double-precision float type are safe for storing this
    /// identifier. The bot can use this identifier for 24 hours to send messages until the join request is processed,
    /// assuming no other administrator contacted the user.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long UserChatId { get; set; }

    /// <summary>
    /// Date the request was sent
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>
    /// Optional. Bio of the user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Bio { get; set; }

    /// <summary>
    /// Optional. Chat invite link that was used by the user to send the join request
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatInviteLink? InviteLink { get; set; }
}
