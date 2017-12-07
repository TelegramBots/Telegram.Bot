using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Payment
{
    [Collection(Constants.TestCollections.Payment)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PaymentTests : IClassFixture<PaymentTestsFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly PaymentTestsFixture _classFixture;

        public PaymentTests(PaymentTestsFixture fixture)
        {
            _classFixture = fixture;
            _fixture = fixture.TestsFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [ExecutionOrder(1.1)]
        public async Task Should_Send_Invoice()
        {
            await _classFixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendInvoice);

            const string payload = "my-payload";

            LabeledPrice[] prices =
            {
                new LabeledPrice() {Amount = 150, Label = "One dollar 50 cents"},
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

            Message message = await BotClient.SendInvoiceAsync(_classFixture.TesterPrivateChatId,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: prices,
                providerData: "{}"
            );

            Assert.Equal(MessageType.Invoice, message.Type);
            Assert.Equal(invoice.Title, message.Invoice.Title);
            Assert.Equal(invoice.Currency, message.Invoice.Currency);
            Assert.Equal(invoice.TotalAmount, message.Invoice.TotalAmount);
            Assert.Equal(invoice.Description, message.Invoice.Description);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerShippingQueryWithOk)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerShippingQuery)]
        [ExecutionOrder(1.2)]
        public async Task Should_Answer_Shipping_Query_With_Ok()
        {
            await _classFixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerShippingQueryWithOk,
                "Click on *Pay <amount>* and send your shipping address. You should see shipment options afterwards.");

            const string payload = "shippingquery-ok-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice {Amount = 150, Label = "One dollar 50 cents"},
                new LabeledPrice {Amount = 2029, Label = "20 dollars 29 cents"},
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            LabeledPrice[] shippingPrices =
            {
                new LabeledPrice {Amount = 500, Label = "SHIPPING1: 500"},
                new LabeledPrice {Amount = 299, Label = "SHIPPING2: 299"},
            };

            ShippingOption[] shippingOptions =
            {
                new ShippingOption
                {
                    Id = "option1",
                    Title = "OPTION-1",
                    Prices = shippingPrices,
                }
            };

            Message message = await _fixture.BotClient.SendInvoiceAsync(_classFixture.TesterPrivateChatId,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: productPrices,
                needShippingAddress: true,
                isFlexible: true
            );

            Update shippingUpdate = await GetShippingQueryUpdate();

            bool result = await _fixture.BotClient.AnswerShippingQueryAsync(
                shippingUpdate.ShippingQuery.Id,
                true,
                shippingOptions
            );

            Assert.Equal(UpdateType.ShippingQueryUpdate, shippingUpdate.Type);
            Assert.Equal(payload, shippingUpdate.ShippingQuery.InvoicePayload);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.CountryCode);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.City);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.State);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.StreetLine1);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.PostCode);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerPreCheckoutQueryWithOkForNoShipmentOption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
        [ExecutionOrder(1.3)]
        public async Task Should_Answer_PreCheckout_Query_With_Ok_For_No_Shipment_Option()
        {
            await _classFixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldAnswerPreCheckoutQueryWithOkForNoShipmentOption,
                "Click on *Pay <amount>* and confirm payment. Transaction should be completed.");

            const string payload = "precheckout-ok-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice {Amount = 150, Label = "One dollar 50 cents"},
                new LabeledPrice {Amount = 2029, Label = "20 dollars 29 cents"},
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "USD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            Message message = await _fixture.BotClient.SendInvoiceAsync(_classFixture.TesterPrivateChatId,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: productPrices
            );

            Update precheckoutUpdate = await GetPreCheckoutQueryUpdate();
            PreCheckoutQuery query = precheckoutUpdate.PreCheckoutQuery;

            bool result = await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
                query.Id,
                true
            );

            Assert.Equal(UpdateType.PreCheckoutQueryUpdate, precheckoutUpdate.Type);
            Assert.NotNull(query.Id);
            Assert.Equal(payload, query.InvoicePayload);
            Assert.Equal(invoice.TotalAmount, query.TotalAmount);
            Assert.Equal(invoice.Currency, query.Currency);
            Assert.Contains(query.From.Username, _fixture.AllowedUserNames);
            Assert.Null(query.OrderInfo);
            Assert.True(result);
        }
        // ToDo: another method: receive successful payment

        [Fact(DisplayName = FactTitles.ShouldThrowWhenSendInvoiceInvalidJson)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [ExecutionOrder(2.1)]
        public async Task Should_Throw_When_Send_Invoice_Invalid_Provider_Data()
        {
            await _classFixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowWhenSendInvoiceInvalidJson);

            const string payload = "my-payload";

            LabeledPrice[] prices =
            {
                new LabeledPrice {Amount = 150, Label = "One dollar 50 cents"},
                new LabeledPrice {Amount = 2029, Label = "20 dollars 29 cents"},
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = prices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                BotClient.SendInvoiceAsync(_classFixture.TesterPrivateChatId,
                    title: invoice.Title,
                    description: invoice.Description,
                    payload: payload,
                    providerToken: _classFixture.PaymentProviderToken,
                    startParameter: invoice.StartParameter,
                    currency: invoice.Currency,
                    prices: prices,
                    providerData: "INVALID-JSON"
            ));

            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: DATA_JSON_INVALID", exception.Message);
        }

        private async Task<Update> GetShippingQueryUpdate(
            CancellationToken cancellationToken = default)
        {
            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.ShippingQueryUpdate);

            Update update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private async Task<Update> GetPreCheckoutQueryUpdate(
            CancellationToken cancellationToken = default)
        {
            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.PreCheckoutQueryUpdate);

            Update update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private async Task<Update> GetSuccessfulPaymentUpdate(
            CancellationToken cancellationToken = default)
        {
            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                predicate: u => u.Message.Type == MessageType.SuccessfulPayment,
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.MessageUpdate);

            Update update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private static class FactTitles
        {
            public const string ShouldSendInvoice = "Should send an invoice";

            public const string ShouldAnswerShippingQueryWithOk =
                "Should receive shipping address query and reply with shipping options";

            public const string ShouldAnswerPreCheckoutQueryWithOkForNoShipmentOption =
                "Should send invoice for no shipment option, and reply pre-checkout query with OK.";

            public const string ShouldThrowWhenSendInvoiceInvalidJson =
                "Should throw exception when sending invoice with invalid provider data";
        }
    }
}