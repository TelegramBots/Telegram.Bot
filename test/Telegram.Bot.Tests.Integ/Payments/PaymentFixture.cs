using System;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;

namespace Telegram.Bot.Tests.Integ.Payments;

public class PaymentFixture : PrivateChatFixture
{
    public string PaymentProviderToken { get; }

    public PaymentFixture(TestsFixture testsFixture)
        : base(testsFixture, Constants.TestCollections.Payment)
    {
        PaymentProviderToken = testsFixture.Configuration.PaymentProviderToken;
        if (PaymentProviderToken is null)
        {
            throw new ArgumentNullException(nameof(PaymentProviderToken));
        }

        if (PaymentProviderToken.Length < 5)
        {
            throw new ArgumentException("Payment provider token is invalid", nameof(PaymentProviderToken));
        }
    }
}