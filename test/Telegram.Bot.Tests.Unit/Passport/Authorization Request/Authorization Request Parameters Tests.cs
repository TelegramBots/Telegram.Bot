// ReSharper disable StringLiteralTypo

using Telegram.Bot.Passport.Request;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Passport.Authorization_Request
{
    public class AuthorizationRequestParametersTests
    {
        [Fact(DisplayName = "Should generate authorization request URI")]
        public void Should_Create_Request_Params()
        {
            const string expectedQuery = "domain=telegrampassport" +
                                         "&bot_id=123" +
                                         "&scope=%7B%22data%22%3A%5B%5D%2C%22v%22%3A1%7D" +
                                         "&public_key=PUB%20KEY" +
                                         "&nonce=%2FNonce%21%2F";

            AuthorizationRequestParameters requestParameters = new AuthorizationRequestParameters(
                123,
                "PUB KEY",
                "/Nonce!/",
                new PassportScope(new IPassportScopeElement[0])
            );

            Assert.Equal(123, requestParameters.BotId);
            Assert.Equal("PUB KEY", requestParameters.PublicKey);
            Assert.Equal("/Nonce!/", requestParameters.Nonce);
            Assert.NotNull(requestParameters.PassportScope);
            Assert.Equal(1, requestParameters.PassportScope.V);
            Assert.Empty(requestParameters.PassportScope.Data);
            Assert.Equal(expectedQuery, requestParameters.Query);
            Assert.Equal("tg:resolve?" + expectedQuery, requestParameters.AndroidUri);
            Assert.Equal("tg://resolve?" + expectedQuery, requestParameters.Uri);
            Assert.Equal("tg://resolve?" + expectedQuery, requestParameters.ToString());
        }
    }
}
