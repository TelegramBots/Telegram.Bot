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
        ArgumentNullException.ThrowIfNull(PaymentProviderToken);
        ArgumentOutOfRangeException.ThrowIfLessThan(PaymentProviderToken.Length, 5);
    }
}
