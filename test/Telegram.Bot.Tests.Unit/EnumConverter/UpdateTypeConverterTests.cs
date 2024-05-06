using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class UpdateTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_UpdateType_Members()
    {
        List<string> updateTypeMembers = Enum
            .GetNames(typeof(UpdateType))
            .OrderBy(x => x)
            .ToList();

        List<string> updateTypeDataMembers = new UpdateTypeData()
            .Select(x => ((WebhookInfo)x[0]).AllowedUpdates!.First().ToString()) // Предполагаем, что AllowedUpdates не пуст и содержит один элемент
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(updateTypeMembers.Count, updateTypeDataMembers.Count);
        Assert.Equal(updateTypeMembers, updateTypeDataMembers);
    }


    [Theory]
    [ClassData(typeof(UpdateTypeData))]
    public void Should_Convert_UpdateType_To_String(WebhookInfo webhookInfo, string value)
    {
        string expectedResult =
            $$"""{"url":"https://example.com","has_custom_certificate":true,"pending_update_count":1,"allowed_updates":["{{value}}"]}""";

        string result = JsonSerializer.Serialize(webhookInfo, TelegramBotClientJsonSerializerContext.Instance.WebhookInfo);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(UpdateTypeData))]
    public void Should_Convert_String_To_UpdateType(WebhookInfo webhookInfo, string value)
    {
        WebhookInfo expectedResult = webhookInfo;
        string jsonData =
            $$"""
              {
                "url": "https://example.com",
                "has_custom_certificate": true,
                "pending_update_count": 1,
                "allowed_updates": ["{{value}}"]
              }
              """;

       WebhookInfo? result  = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.WebhookInfo);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.AllowedUpdates![0], result.AllowedUpdates![0]);
    }

    [Fact]
    public void Should_Return_Unknown_For_Incorrect_UpdateType()
    {
        string jsonData =
            $$"""
            {
              "url": "https://example.com",
              "has_custom_certificate": true,
              "pending_update_count": 1,
              "allowed_updates": [{{int.MaxValue}}]
            }
            """;

        WebhookInfo? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.WebhookInfo);

        Assert.NotNull(result);
        Assert.NotNull(result.AllowedUpdates);
        Assert.Equal(UpdateType.Unknown, result.AllowedUpdates[0]);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_UpdateType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize(
                UpdateTypeData.NewWebhookInfo((UpdateType)int.MaxValue),
                TelegramBotClientJsonSerializerContext.Instance.WebhookInfo));
    }

    // using WebhookInfo here because it is basically the only place where UpdateType is used
    private class UpdateTypeData : IEnumerable<object[]>
    {
        internal static WebhookInfo NewWebhookInfo(UpdateType updateType)
        {
            return new WebhookInfo
            {
                Url = "https://example.com",
                HasCustomCertificate = true,
                PendingUpdateCount = 1,
                AllowedUpdates = [ updateType ],
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewWebhookInfo(UpdateType.Unknown), "unknown"];
            yield return [NewWebhookInfo(UpdateType.Message), "message"];
            yield return [NewWebhookInfo(UpdateType.EditedMessage), "edited_message"];
            yield return [NewWebhookInfo(UpdateType.ChannelPost), "channel_post"];
            yield return [NewWebhookInfo(UpdateType.EditedChannelPost), "edited_channel_post"];
            yield return [NewWebhookInfo(UpdateType.MessageReaction), "message_reaction"];
            yield return [NewWebhookInfo(UpdateType.MessageReactionCount), "message_reaction_count"];
            yield return [NewWebhookInfo(UpdateType.InlineQuery), "inline_query"];
            yield return [NewWebhookInfo(UpdateType.ChosenInlineResult), "chosen_inline_result"];
            yield return [NewWebhookInfo(UpdateType.CallbackQuery), "callback_query"];
            yield return [NewWebhookInfo(UpdateType.ShippingQuery), "shipping_query"];
            yield return [NewWebhookInfo(UpdateType.PreCheckoutQuery), "pre_checkout_query"];
            yield return [NewWebhookInfo(UpdateType.Poll), "poll"];
            yield return [NewWebhookInfo(UpdateType.PollAnswer), "poll_answer"];
            yield return [NewWebhookInfo(UpdateType.MyChatMember), "my_chat_member"];
            yield return [NewWebhookInfo(UpdateType.ChatMember), "chat_member"];
            yield return [NewWebhookInfo(UpdateType.ChatJoinRequest), "chat_join_request"];
            yield return [NewWebhookInfo(UpdateType.ChatBoost), "chat_boost"];
            yield return [NewWebhookInfo(UpdateType.RemovedChatBoost), "removed_chat_boost"];
            yield return [NewWebhookInfo(UpdateType.BusinessConnection), "business_connection"];
            yield return [NewWebhookInfo(UpdateType.BusinessMessage), "business_message"];
            yield return [NewWebhookInfo(UpdateType.EditedBusinessMessage), "edited_business_message"];
            yield return [NewWebhookInfo(UpdateType.DeletedBusinessMessages), "deleted_business_messages"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
