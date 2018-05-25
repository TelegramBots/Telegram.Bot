using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Payments
{
    [Collection(Constants.TestCollections.Payment)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PaymentTests : IClassFixture<PaymentFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly PaymentFixture _classFixture;

        public PaymentTests(TestsFixture fixture, PaymentFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Send_Invoice()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldSendInvoice,
                "Click on *Pay <amount>* and send your shipping address. " +
                "You should see shipment options afterwards. " +
                "Transaction should be completed.",
                chatid: _classFixture.PrivateChat.Id);

            _classFixture.Payload = "my-payload";
            const string url = "https://cdn.pixabay.com/photo/2017/09/07/08/54/money-2724241_640.jpg";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };

            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            Message message = await BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
                title: invoice.Title,
                description: invoice.Description,
                payload: _classFixture.Payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: productPrices,
                photoUrl: url,
                photoWidth: 600,
                photoHeight: 400,
                needShippingAddress: true,
                isFlexible: true,
                needName: true,
                needEmail: true,
                needPhoneNumber: true
            );

            Assert.Equal(MessageType.Invoice, message.Type);
            Assert.Equal(invoice.Title, message.Invoice.Title);
            Assert.Equal(invoice.Currency, message.Invoice.Currency);
            Assert.Equal(invoice.TotalAmount, message.Invoice.TotalAmount);
            Assert.Equal(invoice.Description, message.Invoice.Description);

            _classFixture.Invoice = message.Invoice;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerShippingQueryWithOk)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerShippingQuery)]
        public async Task Should_Answer_Shipping_Query_With_Ok()
        {
            LabeledPrice[] shippingPrices =
            {
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_1", 500),
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_2", 299),
            };

            ShippingOption shippingOption = new ShippingOption
            {
                Id = "option1",
                Title = "OPTION-1",
                Prices = shippingPrices,
            };

            ShippingOption[] shippingOptions =
            {
                shippingOption
            };

            _classFixture.ShippingOption = shippingOption;

            Update shippingUpdate = await GetShippingQueryUpdate();

            await _fixture.BotClient.AnswerShippingQueryAsync(
                shippingQueryId: shippingUpdate.ShippingQuery.Id,
                shippingOptions: shippingOptions
            );

            Assert.Equal(UpdateType.ShippingQuery, shippingUpdate.Type);
            Assert.Equal(_classFixture.Payload, shippingUpdate.ShippingQuery.InvoicePayload);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.CountryCode);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.City);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.State);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.StreetLine1);
            Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.PostCode);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerPreCheckoutQueryWithOkAndShipmentOption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
        public async Task Should_Answer_PreCheckout_Query_With_Ok_And_Shipment_Option()
        {
            Update precheckoutUpdate = await GetPreCheckoutQueryUpdate();
            PreCheckoutQuery query = precheckoutUpdate.PreCheckoutQuery;

            await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
                preCheckoutQueryId: query.Id
            );

            int totalAmount = _classFixture.Invoice.TotalAmount +
                              _classFixture.ShippingOption.Prices.Sum(p => p.Amount);

            Assert.Equal(UpdateType.PreCheckoutQuery, precheckoutUpdate.Type);
            Assert.NotNull(query.Id);
            Assert.Equal(_classFixture.Payload, query.InvoicePayload);
            Assert.Equal(totalAmount, query.TotalAmount);
            Assert.Equal(_classFixture.Invoice.Currency, query.Currency);
            Assert.Contains(query.From.Username, _fixture.UpdateReceiver.AllowedUsernames);
            Assert.NotNull(query.OrderInfo);
            Assert.NotNull(query.OrderInfo.Email);
            Assert.NotNull(query.OrderInfo.Name);
            Assert.NotNull(query.OrderInfo.PhoneNumber);
            Assert.Equal(_classFixture.ShippingOption.Id, query.ShippingOptionId);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldReceiveSuccessfulPaymentWithShipmentOption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
        public async Task Should_Receive_Successful_Payment_With_Shipment_Option()
        {
            Update successfulPaymentUpdate = await GetSuccessfulPaymentUpdate();
            SuccessfulPayment successfulPayment = successfulPaymentUpdate.Message.SuccessfulPayment;

            int totalAmount = _classFixture.Invoice.TotalAmount +
                              _classFixture.ShippingOption.Prices.Sum(p => p.Amount);

            Assert.Equal(totalAmount, successfulPayment.TotalAmount);
            Assert.Equal(_classFixture.Payload, successfulPayment.InvoicePayload);
            Assert.Equal(_classFixture.Invoice.Currency, successfulPayment.Currency);
            Assert.Equal(_classFixture.ShippingOption.Id, successfulPayment.ShippingOptionId);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerShippingQueryWithError)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerShippingQuery)]
        public async Task Should_Answer_Shipping_Query_With_Error()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerShippingQueryWithError,
                "Click on *Pay <amount>* and send your shipping address. You should receive an error afterwards.",
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "shipping_query-error-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "CAD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            await _fixture.BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
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

            await _fixture.BotClient.AnswerShippingQueryAsync(
                shippingQueryId: shippingUpdate.ShippingQuery.Id,
                errorMessage: "HUMAN_FRIENDLY_DELIVERY_ERROR_MESSAGE"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAnswerPreCheckoutQueryWithErrorForNoShipmentOption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
        public async Task Should_Answer_PreCheckout_Query_With_Error_For_No_Shipment_Option()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldAnswerPreCheckoutQueryWithErrorForNoShipmentOption,
                "Click on *Pay <amount>* and confirm payment.",
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "pre_checkout-error-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "USD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            await _fixture.BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
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

            await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
                preCheckoutQueryId: query.Id,
                errorMessage: "HUMAN_FRIENDLY_ERROR_MESSAGE"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowWhenSendInvoiceInvalidJson)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Throw_When_Send_Invoice_Invalid_Provider_Data()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldThrowWhenSendInvoiceInvalidJson,
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "my-payload";

            LabeledPrice[] prices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
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
                BotClient.SendInvoiceAsync(
                    chatId: (int)_classFixture.PrivateChat.Id,
                    title: invoice.Title,
                    description: invoice.Description,
                    payload: payload,
                    providerToken: _classFixture.PaymentProviderToken,
                    startParameter: invoice.StartParameter,
                    currency: invoice.Currency,
                    prices: prices,
                    providerData: "INVALID-JSON"
                ));

            // ToDo: Add exception
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: DATA_JSON_INVALID", exception.Message);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Throw_When_Answer_Shipping_Query_With_Duplicate_Shipping_Id()
        {
            await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId,
                chatid: _classFixture.PrivateChat.Id);

            const string payload = "my-payload";

            LabeledPrice[] productPrices =
            {
                new LabeledPrice("PART_OF_PRODUCT_PRICE_1", 150),
                new LabeledPrice("PART_OF_PRODUCT_PRICE_2", 2029),
            };
            Invoice invoice = new Invoice
            {
                Title = "PRODUCT_TITLE",
                Currency = "USD",
                StartParameter = "start_param",
                TotalAmount = productPrices.Sum(p => p.Amount),
                Description = "PRODUCT_DESCRIPTION",
            };

            await _fixture.BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
                title: invoice.Title,
                description: invoice.Description,
                payload: payload,
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: invoice.StartParameter,
                currency: invoice.Currency,
                prices: productPrices,
                isFlexible: true
            );

            LabeledPrice[] shippingPrices =
            {
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_1", 500),
                new LabeledPrice("PART_OF_SHIPPING_TOTAL_PRICE_2", 299),
            };

            ShippingOption shippingOption = new ShippingOption
            {
                Id = "option1",
                Title = "OPTION-1",
                Prices = shippingPrices,
            };

            Update shippingUpdate = await GetShippingQueryUpdate();

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.AnswerShippingQueryAsync(
                    shippingQueryId: shippingUpdate.ShippingQuery.Id,
                    shippingOptions: new[] { shippingOption, shippingOption }
                )
            );

            // ToDo: Add exception
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: SHIPPING_ID_DUPLICATE", exception.Message);

            await _fixture.BotClient.AnswerShippingQueryAsync(
                shippingQueryId: shippingUpdate.ShippingQuery.Id,
                errorMessage: "✅ Test Passed"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendInvoiceWithReplyMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
        public async Task Should_Send_Invoice_With_Reply_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendInvoiceWithReplyMarkup);

            await BotClient.SendInvoiceAsync(
                chatId: (int)_classFixture.PrivateChat.Id,
                title: "Product",
                description: "product description",
                payload: "test payload",
                providerToken: _classFixture.PaymentProviderToken,
                startParameter: "start_parameter",
                currency: "USD",
                prices: new[] { new LabeledPrice("price", 150), },
                replyMarkup: new InlineKeyboardMarkup(new[] {
                    new []
                    {
                        InlineKeyboardButton.WithPayment("Pay this invoice"),
                        InlineKeyboardButton.WithUrl("Repository", "https://github.com/TelegramBots/Telegram.Bot")
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Some other button")
                    }
                })
            );
        }

        private async Task<Update> GetShippingQueryUpdate(CancellationToken cancellationToken = default)
        {
            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.ShippingQuery);

            Update update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private async Task<Update> GetPreCheckoutQueryUpdate(
            CancellationToken cancellationToken = default)
        {
            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.PreCheckoutQuery);

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
                updateTypes: UpdateType.Message);

            Update update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private static class FactTitles
        {
            public const string ShouldSendInvoice = "Should send an invoice";

            public const string ShouldAnswerShippingQueryWithOk =
                "Should receive shipping address query and reply with shipping options";

            public const string ShouldAnswerPreCheckoutQueryWithOkAndShipmentOption =
                "Should send invoice for no shipment option, and reply pre-checkout query with OK.";

            public const string ShouldReceiveSuccessfulPaymentWithShipmentOption =
                "Should receive successful payment.";

            public const string ShouldAnswerShippingQueryWithError =
                "Should receive shipping address query and reply with an error";

            public const string ShouldAnswerPreCheckoutQueryWithErrorForNoShipmentOption =
                "Should send invoice for no shipment option, and reply pre-checkout query with an error.";

            public const string ShouldThrowWhenSendInvoiceInvalidJson =
                "Should throw exception when sending invoice with invalid provider data";

            public const string ShouldThrowWhenAnswerShippingQueryWithDuplicateShippingId =
                "Should throw exception when answering shipping query with duplicate shipping Id";

            public const string ShouldSendInvoiceWithReplyMarkup = "Should send an invoice with custom reply markup";
        }
    }
}
