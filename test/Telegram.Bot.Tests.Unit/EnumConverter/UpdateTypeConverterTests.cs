using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

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
            .Select(x => Enum.GetName(typeof(UpdateType), x[0]))
            .OrderBy(x => x)
            .ToList()!;

        Assert.Equal(updateTypeMembers.Count, updateTypeDataMembers.Count);
        Assert.Equal(updateTypeMembers, updateTypeDataMembers);
    }


    [Theory]
    [ClassData(typeof(UpdateTypeData))]
    public void Should_Convert_UpdateType_To_String(UpdateType updateType, string value)
    {
        Update update = new(updateType);
        string expectedResult =
            $$"""
            {"type":"{{value}}"}
            """;

        string result = JsonSerializer.Serialize(update, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(UpdateTypeData))]
    public void Should_Convert_String_To_UpdateType(UpdateType updateType, string value)
    {
        Update expectedResult = new(updateType);
        string jsonData =
            $$"""
            {"type":"{{value}}"}
            """;

        Update? result = JsonSerializer.Deserialize<Update>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Unknown_For_Incorrect_UpdateType()
    {
        string jsonData =
            $$"""
            {"type":"{{int.MaxValue}}"}
            """;

        Update? result = JsonSerializer.Deserialize<Update>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(UpdateType.Unknown, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_UpdateType()
    {
        Update update = new((UpdateType)int.MaxValue);

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(update, JsonSerializerOptionsProvider.Options));
    }


    record Update([property: JsonRequired] UpdateType Type);

    private class UpdateTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [UpdateType.Unknown, "unknown"];
            yield return [UpdateType.Message, "message"];
            yield return [UpdateType.EditedMessage, "edited_message"];
            yield return [UpdateType.ChannelPost, "channel_post"];
            yield return [UpdateType.EditedChannelPost, "edited_channel_post"];
            yield return [UpdateType.MessageReaction, "message_reaction"];
            yield return [UpdateType.MessageReactionCount, "message_reaction_count"];
            yield return [UpdateType.InlineQuery, "inline_query"];
            yield return [UpdateType.ChosenInlineResult, "chosen_inline_result"];
            yield return [UpdateType.CallbackQuery, "callback_query"];
            yield return [UpdateType.ShippingQuery, "shipping_query"];
            yield return [UpdateType.PreCheckoutQuery, "pre_checkout_query"];
            yield return [UpdateType.Poll, "poll"];
            yield return [UpdateType.PollAnswer, "poll_answer"];
            yield return [UpdateType.MyChatMember, "my_chat_member"];
            yield return [UpdateType.ChatMember, "chat_member"];
            yield return [UpdateType.ChatJoinRequest, "chat_join_request"];
            yield return [UpdateType.ChatBoost, "chat_boost"];
            yield return [UpdateType.RemovedChatBoost, "removed_chat_boost"];
            yield return [UpdateType.BusinessConnection, "business_connection"];
            yield return [UpdateType.BusinessMessage, "business_message"];
            yield return [UpdateType.EditedBusinessMessage, "edited_business_message"];
            yield return [UpdateType.DeletedBusinessMessages, "deleted_business_messages"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
