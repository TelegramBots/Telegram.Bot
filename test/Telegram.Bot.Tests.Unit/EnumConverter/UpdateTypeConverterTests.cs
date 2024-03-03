using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class UpdateTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_UpdateType_Members()
    {
        List<string> updateTypeMembers = Enum
            .GetNames<UpdateType>()
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

        string result = JsonConvert.SerializeObject(update);

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

        Update? result = JsonConvert.DeserializeObject<Update>(jsonData);

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

        Update? result = JsonConvert.DeserializeObject<Update>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(UpdateType.Unknown, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_UpdateType()
    {
        Update update = new((UpdateType)int.MaxValue);

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(update));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    record Update([property: JsonProperty(Required = Required.Always)] UpdateType Type);

    private class UpdateTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { UpdateType.Unknown, "unknown" };
            yield return new object[] { UpdateType.Message, "message" };
            yield return new object[] { UpdateType.EditedMessage, "edited_message" };
            yield return new object[] { UpdateType.ChannelPost, "channel_post" };
            yield return new object[] { UpdateType.EditedChannelPost, "edited_channel_post" };
            yield return new object[] { UpdateType.MessageReaction, "message_reaction" };
            yield return new object[] { UpdateType.MessageReactionCount, "message_reaction_count" };
            yield return new object[] { UpdateType.InlineQuery, "inline_query" };
            yield return new object[] { UpdateType.ChosenInlineResult, "chosen_inline_result" };
            yield return new object[] { UpdateType.CallbackQuery, "callback_query" };
            yield return new object[] { UpdateType.ShippingQuery, "shipping_query" };
            yield return new object[] { UpdateType.PreCheckoutQuery, "pre_checkout_query" };
            yield return new object[] { UpdateType.Poll, "poll" };
            yield return new object[] { UpdateType.PollAnswer, "poll_answer" };
            yield return new object[] { UpdateType.MyChatMember, "my_chat_member" };
            yield return new object[] { UpdateType.ChatMember, "chat_member" };
            yield return new object[] { UpdateType.ChatJoinRequest, "chat_join_request" };
            yield return new object[] { UpdateType.ChatBoost, "chat_boost" };
            yield return new object[] { UpdateType.RemovedChatBoost, "removed_chat_boost" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
