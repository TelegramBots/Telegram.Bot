using Newtonsoft.Json;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Exceptions.Abstractions;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.ExceptionParser
{
    [Collection("ApiExceptionParser Tests")]
    public class ApiExceptionParserTests
    {
        private readonly IExceptionParser _exceptionParser;

        public ApiExceptionParserTests()
        {
            _exceptionParser = new ApiExceptionParser();
        }

        [Fact(DisplayName = "Should Return UnauthorizedException")]
        public void Should_Return_UnauthorizedException()
        {
            const string responseJson = @"{""ok"":false,""error_code"":401,""description"":""Unauthorized""}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<UnauthorizedException>(exception);
            Assert.Equal(401, exception.ErrorCode);
            Assert.Equal("Unauthorized", exception.Message);
        }

        [Fact(DisplayName = "Should Return BadRequestException")]
        public void Should_Return_BadRequestException()
        {
            const string responseJson = @"{""ok"":false,""error_code"":400,""description"":""Bad Request: message text is empty""}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<BadRequestException>(exception);
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("message text is empty", exception.Message);
        }

        [Fact(DisplayName = "Should Return ForbiddenException")]
        public void Should_Return_ForbiddenException()
        {
            const string responseJson = @"{""ok"": false, ""error_code"": 403, ""description"": ""Forbidden: bot was blocked by the user""}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<ForbiddenException>(exception);
            Assert.Equal(403, exception.ErrorCode);
            Assert.Equal("bot was blocked by the user", exception.Message);
        }

        [Fact(DisplayName = "Should Return TooManyRequestsException")]
        public void Should_Return_TooManyRequestsException()
        {
            const string responseJson = @"{""ok"":false,""error_code"":429,""description"":""Too Many Requests: retry after 1"",""parameters"": {""retry_after"":1}}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<TooManyRequestsException>(exception);
            Assert.Equal(429, exception.ErrorCode);
            Assert.StartsWith("retry after", exception.Message);
            Assert.NotNull(exception.Parameters);
            Assert.Equal(1, exception.Parameters.RetryAfter);
        }

        [Fact(DisplayName = "Should Return ApiRequestException")]
        public void Should_Return_ApiRequestException()
        {
            const string responseJson = @"{""ok"": false, ""error_code"": 404, ""description"": ""Not Found""}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<ApiRequestException>(exception);
            Assert.Equal(404, exception.ErrorCode);
            Assert.Equal("Not Found", exception.Message);
        }

        [Fact(DisplayName = "Should Return ConflictException")]
        public void Should_Return_ConflictException()
        {
            const string responseJson = @"{""ok"":false,""error_code"":409,""description"":""Conflict: can't use getUpdates method while webhook is active""}";
            ApiResponse<string> apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseJson);

            ApiRequestException exception = _exceptionParser.Parse(apiResponse);

            Assert.IsType<ConflictException>(exception);
            Assert.Equal(409, exception.ErrorCode);
            Assert.Equal("can't use getUpdates method while webhook is active", exception.Message);
        }
        //
    }
}
