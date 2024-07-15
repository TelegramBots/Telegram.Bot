using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class ApiResponseSerializationTests
{
    [Fact]
    public void Should_Deserialize_Successful_ApiResponse_With_Value_Type()
    {
        // language=json
        string json = """
        {
            "ok": true,
            "result": true
        }
        """;

        ApiResponse<bool>? apiResponse = JsonSerializer.Deserialize<ApiResponse<bool>>(json, JsonBotAPI.Options);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Ok);
        Assert.True(apiResponse.Result);
    }

    [Fact]
    public void Should_Deserialize_Successful_ApiResponse_With_Reference_Type()
    {
        // language=json
        string json = """
      {
          "ok": true,
          "result": {
            "description": "Test description"
          }
      }
      """;

        ApiResponse<BotDescription>? apiResponse = JsonSerializer.Deserialize<ApiResponse<BotDescription>>(json, JsonBotAPI.Options);

        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Ok);
        Assert.NotNull(apiResponse.Result);
        Assert.Equal("Test description", apiResponse.Result.Description);
    }

    [Fact]
    public void Should_Deserialize_Failed_ApiResponse()
    {
        // language=json
        string json =
        """
        {
            "ok": false,
            "description": "Test error description",
            "error_code": 400,
            "parameters": {
              "migrate_to_chat_id": 1234567890,
              "retry_after": 421
            }
        }
        """;

        ApiResponse<BotDescription>? apiResponse = JsonSerializer.Deserialize<ApiResponse<BotDescription>>(json, JsonBotAPI.Options);

        Assert.NotNull(apiResponse);
        Assert.False(apiResponse.Ok);
        Assert.Null(apiResponse.Result);
        Assert.Equal(400, apiResponse.ErrorCode);
        Assert.Equal("Test error description", apiResponse.Description);
        Assert.NotNull(apiResponse.Parameters);
        Assert.Equal(421, apiResponse.Parameters.RetryAfter);
        Assert.Equal(1234567890, apiResponse.Parameters.MigrateToChatId);
    }
}
