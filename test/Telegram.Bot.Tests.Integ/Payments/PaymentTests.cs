using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;
using PreliminaryInvoice = Telegram.Bot.Tests.Integ.Payments.PaymentsBuilder.PreliminaryInvoice;

namespace Telegram.Bot.Tests.Integ.Payments;

[Collection(Constants.TestCollections.Payment)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class PaymentTests : IClassFixture<PaymentFixture>, IAsyncLifetime
{
    readonly TestsFixture _fixture;
    readonly PaymentFixture _classFixture;

    ITelegramBotClient BotClient => _fixture.BotClient;

    public PaymentTests(TestsFixture fixture, PaymentFixture classFixture)
    {
        _fixture = fixture;
        _classFixture = classFixture;
    }

    [OrderedFact("Should send an invoice")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    public async Task Should_Send_Invoice()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Lunar crater \"Copernicus\"")
                .WithDescription(description:
                    "It was named after the astronomer Nicolaus Copernicus. It may have been created by debris" +
                    " from the breakup of the parent body of asteroid 495 Eulalia 800 million years ago.")
                .WithProductPrice(label: "Price of land inside of the crater", amount: 400_000)
                .WithProductPrice(label: "One-time fee", amount: 10_000)
                .WithPhoto(
                    url: "https://upload.wikimedia.org/wikipedia/commons/d/d4/Copernicus_%28LRO%29_2.png",
                    width: 1264,
                    height: 1264
                ))
            .WithCurrency(currency: "USD")
            .WithStartParameter(startParameter: "crater-copernicus")
            .WithPayload(payload: "<my-payload>")
            .WithPaymentProviderToken(paymentsProviderToken: _classFixture.PaymentProviderToken)
            .ToChat(chatId: _classFixture.PrivateChat.Id);

        PreliminaryInvoice preliminaryInvoice = paymentsBuilder.GetPreliminaryInvoice();
        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        Message message = await BotClient.MakeRequestAsync(requestRequest);
        Invoice invoice = message.Invoice;

        Assert.Equal(MessageType.Invoice, message.Type);
        Assert.NotNull(invoice);
        Assert.Equal(preliminaryInvoice.Title, invoice.Title);
        Assert.Equal(preliminaryInvoice.Currency, invoice.Currency);
        Assert.Equal(preliminaryInvoice.TotalAmount, invoice.TotalAmount);
        Assert.Equal(preliminaryInvoice.Description, invoice.Description);
        Assert.Equal(preliminaryInvoice.StartParameter, invoice.StartParameter);
    }

    [OrderedFact("Should receive shipping address query and reply with shipping options")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerShippingQuery)]
    public async Task Should_Answer_Shipping_Query_With_Ok()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithCurrency(currency: "USD")
            .WithPayload("<my-payload>")
            .WithFlexible()
            .RequireShippingAddress()
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency($"Click on *Pay {totalCostWithoutShippingCost:C}* and send your shipping address.");
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);

        Update shippingUpdate = await GetShippingQueryUpdate();

        AnswerShippingQueryRequest shippingQueryRequest = paymentsBuilder.BuildShippingQueryRequest(
            shippingQueryId: shippingUpdate.ShippingQuery!.Id
        );

        await BotClient.MakeRequestAsync(shippingQueryRequest);

        Assert.Equal(UpdateType.ShippingQuery, shippingUpdate.Type);
        Assert.Equal("<my-payload>", shippingUpdate.ShippingQuery.InvoicePayload);
        Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.CountryCode);
        Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.City);
        Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.State);
        Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.StreetLine1);
        Assert.NotNull(shippingUpdate.ShippingQuery.ShippingAddress.PostCode);
    }

    [OrderedFact("Should send invoice for no shipment option, and reply pre-checkout query with OK.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
    public async Task Should_Answer_PreCheckout_Query_With_Ok_And_Shipment_Option()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithFlexible()
            .RequireShippingAddress()
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}* and send your shipping address. Then click *Pay {totalCostWithoutShippingCost:C}* inside payment dialog. Transaction should be completed."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);

        Update shippingUpdate = await GetShippingQueryUpdate();

        AnswerShippingQueryRequest shippingQueryRequest = paymentsBuilder.BuildShippingQueryRequest(
            shippingQueryId: shippingUpdate.ShippingQuery!.Id
        );

        await BotClient.MakeRequestAsync(shippingQueryRequest);

        Update preCheckoutUpdate = await GetPreCheckoutQueryUpdate();
        PreCheckoutQuery query = preCheckoutUpdate.PreCheckoutQuery;

        await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
            preCheckoutQueryId: query!.Id
        );

        PreliminaryInvoice preliminaryInvoice = paymentsBuilder.GetPreliminaryInvoice();
        int totalAmount = paymentsBuilder.GetTotalAmount();

        Assert.Equal(UpdateType.PreCheckoutQuery, preCheckoutUpdate.Type);
        Assert.NotNull(query.Id);
        Assert.Equal("<my-payload>", query.InvoicePayload);
        Assert.Equal(totalAmount, query.TotalAmount);
        Assert.Equal(preliminaryInvoice.Currency, query.Currency);
        Assert.Contains(query.From.Username, _fixture.UpdateReceiver.AllowedUsernames);
        Assert.NotNull(query.OrderInfo);
        Assert.Null(query.OrderInfo.PhoneNumber);
        Assert.Null(query.OrderInfo.Name);
        Assert.Null(query.OrderInfo.Email);
        Assert.Equal("dhl-express", query.ShippingOptionId);
    }

    [OrderedFact("Should receive successful payment.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
    public async Task Should_Receive_Successful_Payment_With_Shipment_Option()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .RequireEmail()
            .RequireName()
            .RequirePhoneNumber()
            .WithFlexible()
            .RequireShippingAddress()
            .SendEmailToProvider()
            .SendPhoneNumberToProvider()
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}*, send your shipping address and confirm payment. Transaction should be completed."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        Message invoiceMessage = await BotClient.MakeRequestAsync(requestRequest);

        Update shippingUpdate = await GetShippingQueryUpdate();

        AnswerShippingQueryRequest shippingQueryRequest = paymentsBuilder.BuildShippingQueryRequest(
            shippingQueryId: shippingUpdate.ShippingQuery!.Id
        );

        await BotClient.MakeRequestAsync(shippingQueryRequest);

        Update preCheckoutUpdate = await GetPreCheckoutQueryUpdate();
        PreCheckoutQuery query = preCheckoutUpdate.PreCheckoutQuery;

        await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
            preCheckoutQueryId: query!.Id
        );

        Update successfulPaymentUpdate = await GetSuccessfulPaymentUpdate();
        SuccessfulPayment successfulPayment = successfulPaymentUpdate.Message!.SuccessfulPayment;
        int totalAmount = paymentsBuilder.GetTotalAmount();

        Assert.Equal(totalAmount, successfulPayment!.TotalAmount);
        Assert.Equal("<my-payload>", successfulPayment.InvoicePayload);
        Assert.NotNull(invoiceMessage.Invoice);
        Assert.Equal(invoiceMessage.Invoice.Currency, successfulPayment.Currency);
        Assert.Equal("dhl-express", successfulPayment.ShippingOptionId);
        Assert.NotNull(successfulPayment.OrderInfo);
        Assert.NotNull(successfulPayment.OrderInfo.Email);
        Assert.NotNull(successfulPayment.OrderInfo.Name);
        Assert.NotNull(successfulPayment.OrderInfo.PhoneNumber);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress.City);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress.CountryCode);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress.PostCode);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress.StreetLine1);
        Assert.NotNull(successfulPayment.OrderInfo.ShippingAddress.StreetLine2);
    }

    [OrderedFact("Should receive successful payment with a tip")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
    public async Task Should_Receive_Successful_Payment_With_A_Tip()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Three tasty donuts")
                .WithDescription(description: "Donuts with special glaze")
                .WithProductPrice(label: "Donut with chocolate glaze", amount: 550)
                .WithProductPrice(label: "Donut with vanilla glaze", amount: 500)
                .WithProductPrice(label: "Donut with glaze", amount: 500)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2017/11/22/00/18/donuts-2969490_960_720.jpg",
                    width: 960,
                    height: 640
                ))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithSuggestedTips(100, 150, 200)
            .WithMaxTip(maxTipAmount: 300)
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}*, select a tip from given options and confirm payment. Transaction should be completed."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();
        Message invoiceMessage = await BotClient.MakeRequestAsync(requestRequest);
        Update preCheckoutUpdate = await GetPreCheckoutQueryUpdate();
        PreCheckoutQuery query = preCheckoutUpdate.PreCheckoutQuery;

        await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
            preCheckoutQueryId: query!.Id
        );

        Update successfulPaymentUpdate = await GetSuccessfulPaymentUpdate();
        SuccessfulPayment successfulPayment = successfulPaymentUpdate.Message!.SuccessfulPayment;
        int totalAmount = paymentsBuilder.GetTotalAmount();

        int[] suggestedTips = {100, 150, 200};
        int[] totalAmountWithTip = suggestedTips.Select(_ => _ + totalAmount).ToArray();

        Assert.Contains(totalAmountWithTip, _ => _ == successfulPayment!.TotalAmount);
        Assert.Equal("<my-payload>", successfulPayment!.InvoicePayload);
        Assert.NotNull(invoiceMessage.Invoice);
        Assert.Equal(invoiceMessage.Invoice.Currency, successfulPayment.Currency);
    }

    [OrderedFact("Should receive shipping address query and reply with an error")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerShippingQuery)]
    public async Task Should_Answer_Shipping_Query_With_Error()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithFlexible()
            .RequireShippingAddress()
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}* and send your shipping address. You will receive an error. Close payment window after that."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);

        Update shippingUpdate = await GetShippingQueryUpdate();

        AnswerShippingQueryRequest shippingQueryRequest = paymentsBuilder.BuildShippingQueryRequest(
            shippingQueryId: shippingUpdate.ShippingQuery!.Id,
            errorMessage: "Sorry, but we don't ship to your contry."
        );

        await BotClient.MakeRequestAsync(shippingQueryRequest);
    }

    [OrderedFact("Should send invoice for no shipment option, and reply pre-checkout query with an error.")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerPreCheckoutQuery)]
    public async Task Should_Answer_PreCheckout_Query_With_Error_For_No_Shipment_Option()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}* and confirm payment. You should receive an error. Close payment window after that."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);

        Update preCheckoutUpdate = await GetPreCheckoutQueryUpdate();
        PreCheckoutQuery query = preCheckoutUpdate.PreCheckoutQuery;

        await _fixture.BotClient.AnswerPreCheckoutQueryAsync(
            preCheckoutQueryId: query!.Id,
            errorMessage: "Sorry, we couldn't process the transaction. Please, contact our support."
        );
    }

    [OrderedFact("Should throw exception when sending invoice with invalid provider data")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    public async Task Should_Throw_When_Send_Invoice_Invalid_Provider_Data()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithProviderData("INVALID-JSON")
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(
            async () => await BotClient.MakeRequestAsync(requestRequest)
        );

        Assert.Equal(400, exception.ErrorCode);
        Assert.Equal("Bad Request: DATA_JSON_INVALID", exception.Message);
    }

    [OrderedFact("Should throw exception when answering shipping query with duplicate shipping Id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    public async Task Should_Throw_When_Answer_Shipping_Query_With_Duplicate_Shipping_Id()
    {
        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithShipping(_ => _
                .WithTitle(title: "DHL Express (Duplicate)")
                .WithId(id: "dhl-express")
                .WithPrice(label: "Packaging", amount: 400_000)
                .WithPrice(label: "Shipping price", amount: 337_600))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithFlexible()
            .RequireShippingAddress()
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        double totalCostWithoutShippingCost = paymentsBuilder
            .GetTotalAmountWithoutShippingCost()
            .CurrencyFormat();

        string instruction = FormatInstructionWithCurrency(
            $"Click on *Pay {totalCostWithoutShippingCost:C}*, send your shipping address. You should receive an error."
        );
        await _fixture.SendTestInstructionsAsync(instruction, chatId: _classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);

        Update shippingUpdate = await GetShippingQueryUpdate();

        AnswerShippingQueryRequest shippingQueryRequest = paymentsBuilder.BuildShippingQueryRequest(
            shippingQueryId: shippingUpdate.ShippingQuery!.Id
        );

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(
            async () => await BotClient.MakeRequestAsync(shippingQueryRequest)
        );

        Assert.Equal(400, exception.ErrorCode);
        Assert.Equal("Bad Request: SHIPPING_ID_DUPLICATE", exception.Message);

        await _fixture.BotClient.AnswerShippingQueryAsync(
            shippingQueryId: shippingUpdate.ShippingQuery.Id,
            errorMessage: "✅ Test Passed"
        );
    }

    [OrderedFact("Should send an invoice with custom reply markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendInvoice)]
    public async Task Should_Send_Invoice_With_Reply_Markup()
    {
        InlineKeyboardMarkup replyMarkup = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithPayment("Pay this invoice"),
                InlineKeyboardButton.WithUrl("Repository", "https://github.com/TelegramBots/Telegram.Bot")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Some other button")
            }
        });

        PaymentsBuilder paymentsBuilder = new PaymentsBuilder()
            .WithProduct(_ => _
                .WithTitle(title: "Reproduction of \"La nascita di Venere\"")
                .WithDescription(description:
                    "Sandro Botticelli’s the Birth of Venus depicts the goddess Venus arriving at the shore" +
                    " after her birth, when she had emerged from the sea fully-grown ")
                .WithProductPrice(label: "Price of the painting", amount: 500_000)
                .WithProductPrice(label: "Wooden frame", amount: 100_000)
                .WithPhoto(
                    url: "https://cdn.pixabay.com/photo/2012/10/26/03/16/painting-63186_1280.jpg",
                    width: 1280,
                    height: 820
                ))
            .WithCurrency("USD")
            .WithPayload("<my-payload>")
            .WithReplyMarkup(replyMarkup)
            .WithPaymentProviderToken(_classFixture.PaymentProviderToken)
            .ToChat(_classFixture.PrivateChat.Id);

        SendInvoiceRequest requestRequest = paymentsBuilder.BuildInvoiceRequest();

        await BotClient.MakeRequestAsync(requestRequest);
    }

    async Task<Update> GetShippingQueryUpdate(CancellationToken cancellationToken = default)
    {
        Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
            cancellationToken: cancellationToken,
            updateTypes: UpdateType.ShippingQuery
        );

        Update update = updates.Single();

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

        return update;
    }

    async Task<Update> GetPreCheckoutQueryUpdate(CancellationToken cancellationToken = default)
    {
        Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
            cancellationToken: cancellationToken,
            updateTypes: UpdateType.PreCheckoutQuery);

        Update update = updates.Single();

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

        return update;
    }

    async Task<Update> GetSuccessfulPaymentUpdate(CancellationToken cancellationToken = default)
    {
        Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
            predicate: u => u.Message?.Type == MessageType.SuccessfulPayment,
            cancellationToken: cancellationToken,
            updateTypes: UpdateType.Message
        );

        Update update = updates.Single();

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

        return update;
    }

    public async Task InitializeAsync() => await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    public static string FormatInstructionWithCurrency(FormattableString instruction) =>
        instruction.ToString(CultureInfo.GetCultureInfo("en-US"));
}

public static class Extensions
{
    // ReSharper disable once PossibleLossOfFraction
    public static double CurrencyFormat(this int amount) => amount * 0.01;
}
