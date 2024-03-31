using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Framework;

public static class Asserts
{
    public static void JsonEquals(
        object expected,
        object actual,
        params string[] excludeFields)
    {
        if (expected is null || actual is null)
        {
            Assert.Equal(expected, actual);
        }
        else
        {
            JsonNode expectedNode = JsonSerializer.SerializeToNode(expected, JsonSerializerOptionsProvider.Options);
            JsonNode actualNode = JsonSerializer.SerializeToNode(actual, JsonSerializerOptionsProvider.Options);

            if (expectedNode is null) throw new ArgumentException("Couldn't serialize expected object");
            if (actualNode is null) throw new ArgumentException("Couldn't serialize actual object");

            foreach (var excludeField in excludeFields)
            {
                if (expectedNode is JsonObject expectedObject && actualNode is JsonObject actualObject)
                {
                    expectedObject.Remove(excludeField);
                    actualObject.Remove(excludeField);
                }
            }

            bool equals = JsonNode.DeepEquals(expectedNode, actualNode);;

            if (equals)
            {
                Assert.True(equals);
            }
            else
            {
                // Print out both JSON values in the case of an inequality
                string expectedJson = JsonSerializer.Serialize(expectedNode, JsonSerializerOptionsProvider.Options);
                string actualJson = JsonSerializer.Serialize(actualNode, JsonSerializerOptionsProvider.Options);
                Assert.Equal(expectedJson, actualJson);
            }
        }
    }

    // getMe request returns more information than is present in received updates
    public static void UsersEqual(User expected, User actual)
    {
        JsonEquals(
            expected: expected,
            actual: actual,
            excludeFields:
            [
                "can_join_groups",
                "can_read_all_group_messages",
                "supports_inline_queries"
            ]
        );
    }
}
