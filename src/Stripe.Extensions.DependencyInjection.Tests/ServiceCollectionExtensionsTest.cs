using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe.Radar;
using Stripe.Reporting;
using Stripe.Sigma;
using Stripe.Terminal;
using Stripe.TestHelpers;
using Stripe.Treasury;
using Xunit;

namespace Stripe.Extensions.DependencyInjection.Tests;

public class ServiceCollectionExtensionsTest
{
    [Theory]
    [InlineData(typeof(AccountLinkService))]
    [InlineData(typeof(AccountService))]
    [InlineData(typeof(ApplePayDomainService))]
    [InlineData(typeof(ApplicationFeeService))]
    [InlineData(typeof(Apps.SecretService))]
    [InlineData(typeof(BalanceService))]
    [InlineData(typeof(BalanceTransactionService))]
    [InlineData(typeof(BillingPortal.ConfigurationService))]
    [InlineData(typeof(ConfigurationService))]
    [InlineData(typeof(Checkout.SessionService))]
    [InlineData(typeof(BillingPortal.SessionService))]
    [InlineData(typeof(ChargeService))]
    [InlineData(typeof(CountrySpecService))]
    [InlineData(typeof(CouponService))]
    [InlineData(typeof(CreditNoteService))]
    [InlineData(typeof(CustomerService))]
    [InlineData(typeof(DiscountService))]
    [InlineData(typeof(DisputeService))]
    [InlineData(typeof(EphemeralKeyService))]
    [InlineData(typeof(EventService))]
    [InlineData(typeof(FileLinkService))]
    [InlineData(typeof(FileService))]
    [InlineData(typeof(Identity.VerificationReportService))]
    [InlineData(typeof(Identity.VerificationSessionService))]
    [InlineData(typeof(InvoiceItemService))]
    [InlineData(typeof(InvoiceService))]
    [InlineData(typeof(Issuing.AuthorizationService))]
    [InlineData(typeof(Issuing.CardholderService))]
    [InlineData(typeof(CardService))]
    [InlineData(typeof(Issuing.TransactionService))]
    [InlineData(typeof(TransactionService))]
    [InlineData(typeof(MandateService))]
    [InlineData(typeof(OAuthTokenService))]
    [InlineData(typeof(PaymentIntentService))]
    [InlineData(typeof(PaymentLinkService))]
    [InlineData(typeof(PaymentMethodService))]
    [InlineData(typeof(PayoutService))]
    [InlineData(typeof(PlanService))]
    [InlineData(typeof(PriceService))]
    [InlineData(typeof(ProductService))]
    [InlineData(typeof(PromotionCodeService))]
    [InlineData(typeof(QuoteService))]
    [InlineData(typeof(EarlyFraudWarningService))]
    [InlineData(typeof(ValueListItemService))]
    [InlineData(typeof(ValueListService))]
    [InlineData(typeof(RefundService))]
    [InlineData(typeof(ReportRunService))]
    [InlineData(typeof(ReportTypeService))]
    [InlineData(typeof(ReviewService))]
    [InlineData(typeof(SetupAttemptService))]
    [InlineData(typeof(SetupIntentService))]
    [InlineData(typeof(ShippingRateService))]
    [InlineData(typeof(ScheduledQueryRunService))]
    [InlineData(typeof(SourceService))]
    [InlineData(typeof(SubscriptionItemService))]
    [InlineData(typeof(SubscriptionScheduleService))]
    [InlineData(typeof(SubscriptionService))]
    [InlineData(typeof(TaxCodeService))]
    [InlineData(typeof(TaxRateService))]
    [InlineData(typeof(ConnectionTokenService))]
    [InlineData(typeof(LocationService))]
    [InlineData(typeof(ReaderService))]
    [InlineData(typeof(TestClockService))]
    [InlineData(typeof(InboundTransferService))]
    [InlineData(typeof(OutboundPaymentService))]
    [InlineData(typeof(OutboundTransferService))]
    [InlineData(typeof(ReceivedCreditService))]
    [InlineData(typeof(ReceivedDebitService))]
    [InlineData(typeof(TokenService))]
    [InlineData(typeof(TopupService))]
    [InlineData(typeof(TransferService))]
    [InlineData(typeof(CreditReversalService))]
    [InlineData(typeof(DebitReversalService))]
    [InlineData(typeof(FinancialAccountService))]
    [InlineData(typeof(TransactionEntryService))]
    [InlineData(typeof(WebhookEndpointService))]
    public void CanResolveStripeServicesFromServiceProvider(Type serviceType)
    {
        var collection = new ServiceCollection();
        collection.AddStripe("someAPIkey");

        var provider = collection.BuildServiceProvider();
        Assert.NotNull(provider.GetRequiredService(serviceType));
    }

    [Fact]
    public void ReadsKeyInformationFromDefaultConfigSection()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<IConfiguration>(
            new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "Stripe:SecretKey", "MyKey" }
                }).Build());
        collection.AddStripe();

        var provider = collection.BuildServiceProvider();
        var priceService = provider.GetRequiredService<PriceService>();

        Assert.Equal("MyKey", priceService.Client.ApiKey);
    }

    [Fact]
    public void UsesApiKeyProvidedDuringRegistration()
    {
        var collection = new ServiceCollection();
        collection.AddStripe("MyKey");

        var provider = collection.BuildServiceProvider();
        var priceService = provider.GetRequiredService<PriceService>();

        Assert.Equal("MyKey", priceService.Client.ApiKey);
    }
}
