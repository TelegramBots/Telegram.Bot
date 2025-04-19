// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents the rights of a business bot.</summary>
public partial class BusinessBotRights
{
    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can send and edit messages in the private chats that had incoming messages in the last 24 hours</summary>
    [JsonPropertyName("can_reply")]
    public bool CanReply { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can mark incoming private messages as read</summary>
    [JsonPropertyName("can_read_messages")]
    public bool CanReadMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can delete messages sent by the bot</summary>
    [JsonPropertyName("can_delete_sent_messages")]
    public bool CanDeleteSentMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can delete all private messages in managed chats</summary>
    [JsonPropertyName("can_delete_all_messages")]
    public bool CanDeleteAllMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can edit the first and last name of the business account</summary>
    [JsonPropertyName("can_edit_name")]
    public bool CanEditName { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can edit the bio of the business account</summary>
    [JsonPropertyName("can_edit_bio")]
    public bool CanEditBio { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can edit the profile photo of the business account</summary>
    [JsonPropertyName("can_edit_profile_photo")]
    public bool CanEditProfilePhoto { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can edit the username of the business account</summary>
    [JsonPropertyName("can_edit_username")]
    public bool CanEditUsername { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can change the privacy settings pertaining to gifts for the business account</summary>
    [JsonPropertyName("can_change_gift_settings")]
    public bool CanChangeGiftSettings { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can view gifts and the amount of Telegram Stars owned by the business account</summary>
    [JsonPropertyName("can_view_gifts_and_stars")]
    public bool CanViewGiftsAndStars { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can convert regular gifts owned by the business account to Telegram Stars</summary>
    [JsonPropertyName("can_convert_gifts_to_stars")]
    public bool CanConvertGiftsToStars { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can transfer and upgrade gifts owned by the business account</summary>
    [JsonPropertyName("can_transfer_and_upgrade_gifts")]
    public bool CanTransferAndUpgradeGifts { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can transfer Telegram Stars received by the business account to its own account, or use them to upgrade and transfer gifts</summary>
    [JsonPropertyName("can_transfer_stars")]
    public bool CanTransferStars { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the bot can post, edit and delete stories on behalf of the business account</summary>
    [JsonPropertyName("can_manage_stories")]
    public bool CanManageStories { get; set; }
}
