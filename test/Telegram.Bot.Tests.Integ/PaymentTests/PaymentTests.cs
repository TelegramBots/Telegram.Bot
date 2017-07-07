using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Xunit;

namespace Telegram.Bot.Tests.Integ.PaymentTests
{
    [Collection(CommonConstants.TestCollectionName)]
    [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.Payments)]
    public class PaymentTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly BotClientFixture _fixture;

        public PaymentTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendInvoice)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendInvoice)]
        public async Task ShouldSendInvoice()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldSendInvoice);

            const string payload = "my-payload";

            LabeledPrice[] prices = {
                new LabeledPrice() {Amount = 150, Label = "One dolloar 50 cents"},
                new LabeledPrice() {Amount = 2029, Label = "20 dollars 29 cents"},
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = prices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            Message message = await BotClient.SendInvoiceAsync(_fixture.ChatId,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _fixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: prices
                );

            Assert.Equal(MessageType.Invoice, message.Type);
            Assert.Equal(invoice.Title, message.Invoice.Title);
            Assert.Equal(invoice.Currency, message.Invoice.Currency);
            Assert.Equal(invoice.TotalAmount, message.Invoice.TotalAmount);
            Assert.Equal(invoice.Description, message.Invoice.Description);
        }

        private async Task<Update> GetPreCheckoutQueryUpdate(CancellationToken cancellationToken = default(CancellationToken))
        {
            var updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.PreCheckoutQueryUpdate);

            var update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private static class FactTitles
        {
            public const string ShouldSendInvoice = "Should send an invoice";
        }
    }
}
