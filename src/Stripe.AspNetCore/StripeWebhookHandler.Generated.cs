namespace Stripe.AspNetCore;
public partial class StripeWebhookHandler
{
    /// Fired when the account.application.authorized event is received.
    public virtual Task OnAccountApplicationAuthorizedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the account.application.deauthorized event is received.
    public virtual Task OnAccountApplicationDeauthorizedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the account.external_account.created event is received.
    public virtual Task OnAccountExternalAccountCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the account.external_account.deleted event is received.
    public virtual Task OnAccountExternalAccountDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the account.external_account.updated event is received.
    public virtual Task OnAccountExternalAccountUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the account.updated event is received.
    public virtual Task OnAccountUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the application_fee.created event is received.
    public virtual Task OnApplicationFeeCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the application_fee.refund.updated event is received.
    public virtual Task OnApplicationFeeRefundUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the application_fee.refunded event is received.
    public virtual Task OnApplicationFeeRefundedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the balance.available event is received.
    public virtual Task OnBalanceAvailableAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the billing_portal.configuration.created event is received.
    public virtual Task OnBillingPortalConfigurationCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the billing_portal.configuration.updated event is received.
    public virtual Task OnBillingPortalConfigurationUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the billing_portal.session.created event is received.
    public virtual Task OnBillingPortalSessionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the capability.updated event is received.
    public virtual Task OnCapabilityUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the cash_balance.funds_available event is received.
    public virtual Task OnCashBalanceFundsAvailableAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.captured event is received.
    public virtual Task OnChargeCapturedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.dispute.closed event is received.
    public virtual Task OnChargeDisputeClosedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.dispute.created event is received.
    public virtual Task OnChargeDisputeCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.dispute.funds_reinstated event is received.
    public virtual Task OnChargeDisputeFundsReinstatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.dispute.funds_withdrawn event is received.
    public virtual Task OnChargeDisputeFundsWithdrawnAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.dispute.updated event is received.
    public virtual Task OnChargeDisputeUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.expired event is received.
    public virtual Task OnChargeExpiredAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.failed event is received.
    public virtual Task OnChargeFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.pending event is received.
    public virtual Task OnChargePendingAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.refund.updated event is received.
    public virtual Task OnChargeRefundUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.refunded event is received.
    public virtual Task OnChargeRefundedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.succeeded event is received.
    public virtual Task OnChargeSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the charge.updated event is received.
    public virtual Task OnChargeUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the checkout.session.async_payment_failed event is received.
    public virtual Task OnCheckoutSessionAsyncPaymentFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the checkout.session.async_payment_succeeded event is received.
    public virtual Task OnCheckoutSessionAsyncPaymentSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the checkout.session.completed event is received.
    public virtual Task OnCheckoutSessionCompletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the checkout.session.expired event is received.
    public virtual Task OnCheckoutSessionExpiredAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the coupon.created event is received.
    public virtual Task OnCouponCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the coupon.deleted event is received.
    public virtual Task OnCouponDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the coupon.updated event is received.
    public virtual Task OnCouponUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the credit_note.created event is received.
    public virtual Task OnCreditNoteCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the credit_note.updated event is received.
    public virtual Task OnCreditNoteUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the credit_note.voided event is received.
    public virtual Task OnCreditNoteVoidedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.created event is received.
    public virtual Task OnCustomerCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.deleted event is received.
    public virtual Task OnCustomerDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.discount.created event is received.
    public virtual Task OnCustomerDiscountCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.discount.deleted event is received.
    public virtual Task OnCustomerDiscountDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.discount.updated event is received.
    public virtual Task OnCustomerDiscountUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.source.created event is received.
    public virtual Task OnCustomerSourceCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.source.deleted event is received.
    public virtual Task OnCustomerSourceDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.source.expiring event is received.
    public virtual Task OnCustomerSourceExpiringAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.source.updated event is received.
    public virtual Task OnCustomerSourceUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.created event is received.
    public virtual Task OnCustomerSubscriptionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.deleted event is received.
    public virtual Task OnCustomerSubscriptionDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.pending_update_applied event is received.
    public virtual Task OnCustomerSubscriptionPendingUpdateAppliedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.pending_update_expired event is received.
    public virtual Task OnCustomerSubscriptionPendingUpdateExpiredAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.trial_will_end event is received.
    public virtual Task OnCustomerSubscriptionTrialWillEndAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.subscription.updated event is received.
    public virtual Task OnCustomerSubscriptionUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.tax_id.created event is received.
    public virtual Task OnCustomerTaxIdCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.tax_id.deleted event is received.
    public virtual Task OnCustomerTaxIdDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.tax_id.updated event is received.
    public virtual Task OnCustomerTaxIdUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer.updated event is received.
    public virtual Task OnCustomerUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the customer_cash_balance_transaction.created event is received.
    public virtual Task OnCustomerCashBalanceTransactionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the file.created event is received.
    public virtual Task OnFileCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the financial_connections.account.created event is received.
    public virtual Task OnFinancialConnectionsAccountCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the financial_connections.account.deactivated event is received.
    public virtual Task OnFinancialConnectionsAccountDeactivatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the financial_connections.account.disconnected event is received.
    public virtual Task OnFinancialConnectionsAccountDisconnectedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the financial_connections.account.reactivated event is received.
    public virtual Task OnFinancialConnectionsAccountReactivatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the financial_connections.account.refreshed_balance event is received.
    public virtual Task OnFinancialConnectionsAccountRefreshedBalanceAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.canceled event is received.
    public virtual Task OnIdentityVerificationSessionCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.created event is received.
    public virtual Task OnIdentityVerificationSessionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.processing event is received.
    public virtual Task OnIdentityVerificationSessionProcessingAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.redacted event is received.
    public virtual Task OnIdentityVerificationSessionRedactedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.requires_input event is received.
    public virtual Task OnIdentityVerificationSessionRequiresInputAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the identity.verification_session.verified event is received.
    public virtual Task OnIdentityVerificationSessionVerifiedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.created event is received.
    public virtual Task OnInvoiceCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.deleted event is received.
    public virtual Task OnInvoiceDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.finalization_failed event is received.
    public virtual Task OnInvoiceFinalizationFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.finalized event is received.
    public virtual Task OnInvoiceFinalizedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.marked_uncollectible event is received.
    public virtual Task OnInvoiceMarkedUncollectibleAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.paid event is received.
    public virtual Task OnInvoicePaidAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.payment_action_required event is received.
    public virtual Task OnInvoicePaymentActionRequiredAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.payment_failed event is received.
    public virtual Task OnInvoicePaymentFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.payment_succeeded event is received.
    public virtual Task OnInvoicePaymentSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.sent event is received.
    public virtual Task OnInvoiceSentAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.upcoming event is received.
    public virtual Task OnInvoiceUpcomingAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.updated event is received.
    public virtual Task OnInvoiceUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoice.voided event is received.
    public virtual Task OnInvoiceVoidedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoiceitem.created event is received.
    public virtual Task OnInvoiceitemCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoiceitem.deleted event is received.
    public virtual Task OnInvoiceitemDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the invoiceitem.updated event is received.
    public virtual Task OnInvoiceitemUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_authorization.created event is received.
    public virtual Task OnIssuingAuthorizationCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_authorization.request event is received.
    public virtual Task OnIssuingAuthorizationRequestAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_authorization.updated event is received.
    public virtual Task OnIssuingAuthorizationUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_card.created event is received.
    public virtual Task OnIssuingCardCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_card.updated event is received.
    public virtual Task OnIssuingCardUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_cardholder.created event is received.
    public virtual Task OnIssuingCardholderCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_cardholder.updated event is received.
    public virtual Task OnIssuingCardholderUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_dispute.closed event is received.
    public virtual Task OnIssuingDisputeClosedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_dispute.created event is received.
    public virtual Task OnIssuingDisputeCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_dispute.funds_reinstated event is received.
    public virtual Task OnIssuingDisputeFundsReinstatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_dispute.submitted event is received.
    public virtual Task OnIssuingDisputeSubmittedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_dispute.updated event is received.
    public virtual Task OnIssuingDisputeUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_transaction.created event is received.
    public virtual Task OnIssuingTransactionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the issuing_transaction.updated event is received.
    public virtual Task OnIssuingTransactionUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the mandate.updated event is received.
    public virtual Task OnMandateUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the order.created event is received.
    public virtual Task OnOrderCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.amount_capturable_updated event is received.
    public virtual Task OnPaymentIntentAmountCapturableUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.canceled event is received.
    public virtual Task OnPaymentIntentCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.created event is received.
    public virtual Task OnPaymentIntentCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.partially_funded event is received.
    public virtual Task OnPaymentIntentPartiallyFundedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.payment_failed event is received.
    public virtual Task OnPaymentIntentPaymentFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.processing event is received.
    public virtual Task OnPaymentIntentProcessingAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.requires_action event is received.
    public virtual Task OnPaymentIntentRequiresActionAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_intent.succeeded event is received.
    public virtual Task OnPaymentIntentSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_link.created event is received.
    public virtual Task OnPaymentLinkCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_link.updated event is received.
    public virtual Task OnPaymentLinkUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_method.attached event is received.
    public virtual Task OnPaymentMethodAttachedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_method.automatically_updated event is received.
    public virtual Task OnPaymentMethodAutomaticallyUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_method.detached event is received.
    public virtual Task OnPaymentMethodDetachedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payment_method.updated event is received.
    public virtual Task OnPaymentMethodUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payout.canceled event is received.
    public virtual Task OnPayoutCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payout.created event is received.
    public virtual Task OnPayoutCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payout.failed event is received.
    public virtual Task OnPayoutFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payout.paid event is received.
    public virtual Task OnPayoutPaidAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the payout.updated event is received.
    public virtual Task OnPayoutUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the person.created event is received.
    public virtual Task OnPersonCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the person.deleted event is received.
    public virtual Task OnPersonDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the person.updated event is received.
    public virtual Task OnPersonUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the plan.created event is received.
    public virtual Task OnPlanCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the plan.deleted event is received.
    public virtual Task OnPlanDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the plan.updated event is received.
    public virtual Task OnPlanUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the price.created event is received.
    public virtual Task OnPriceCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the price.deleted event is received.
    public virtual Task OnPriceDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the price.updated event is received.
    public virtual Task OnPriceUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the product.created event is received.
    public virtual Task OnProductCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the product.deleted event is received.
    public virtual Task OnProductDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the product.updated event is received.
    public virtual Task OnProductUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the promotion_code.created event is received.
    public virtual Task OnPromotionCodeCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the promotion_code.updated event is received.
    public virtual Task OnPromotionCodeUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the quote.accepted event is received.
    public virtual Task OnQuoteAcceptedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the quote.canceled event is received.
    public virtual Task OnQuoteCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the quote.created event is received.
    public virtual Task OnQuoteCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the quote.finalized event is received.
    public virtual Task OnQuoteFinalizedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the radar.early_fraud_warning.created event is received.
    public virtual Task OnRadarEarlyFraudWarningCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the radar.early_fraud_warning.updated event is received.
    public virtual Task OnRadarEarlyFraudWarningUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the recipient.created event is received.
    public virtual Task OnRecipientCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the recipient.deleted event is received.
    public virtual Task OnRecipientDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the recipient.updated event is received.
    public virtual Task OnRecipientUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the reporting.report_run.failed event is received.
    public virtual Task OnReportingReportRunFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the reporting.report_run.succeeded event is received.
    public virtual Task OnReportingReportRunSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the reporting.report_type.updated event is received.
    public virtual Task OnReportingReportTypeUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the review.closed event is received.
    public virtual Task OnReviewClosedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the review.opened event is received.
    public virtual Task OnReviewOpenedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the setup_intent.canceled event is received.
    public virtual Task OnSetupIntentCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the setup_intent.created event is received.
    public virtual Task OnSetupIntentCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the setup_intent.requires_action event is received.
    public virtual Task OnSetupIntentRequiresActionAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the setup_intent.setup_failed event is received.
    public virtual Task OnSetupIntentSetupFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the setup_intent.succeeded event is received.
    public virtual Task OnSetupIntentSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the sigma.scheduled_query_run.created event is received.
    public virtual Task OnSigmaScheduledQueryRunCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the sku.created event is received.
    public virtual Task OnSkuCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the sku.deleted event is received.
    public virtual Task OnSkuDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the sku.updated event is received.
    public virtual Task OnSkuUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.canceled event is received.
    public virtual Task OnSourceCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.chargeable event is received.
    public virtual Task OnSourceChargeableAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.failed event is received.
    public virtual Task OnSourceFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.mandate_notification event is received.
    public virtual Task OnSourceMandateNotificationAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.refund_attributes_required event is received.
    public virtual Task OnSourceRefundAttributesRequiredAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.transaction.created event is received.
    public virtual Task OnSourceTransactionCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the source.transaction.updated event is received.
    public virtual Task OnSourceTransactionUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.aborted event is received.
    public virtual Task OnSubscriptionScheduleAbortedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.canceled event is received.
    public virtual Task OnSubscriptionScheduleCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.completed event is received.
    public virtual Task OnSubscriptionScheduleCompletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.created event is received.
    public virtual Task OnSubscriptionScheduleCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.expiring event is received.
    public virtual Task OnSubscriptionScheduleExpiringAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.released event is received.
    public virtual Task OnSubscriptionScheduleReleasedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the subscription_schedule.updated event is received.
    public virtual Task OnSubscriptionScheduleUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the tax_rate.created event is received.
    public virtual Task OnTaxRateCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the tax_rate.updated event is received.
    public virtual Task OnTaxRateUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the terminal.reader.action_failed event is received.
    public virtual Task OnTerminalReaderActionFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the terminal.reader.action_succeeded event is received.
    public virtual Task OnTerminalReaderActionSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the test_helpers.test_clock.advancing event is received.
    public virtual Task OnTestHelpersTestClockAdvancingAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the test_helpers.test_clock.created event is received.
    public virtual Task OnTestHelpersTestClockCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the test_helpers.test_clock.deleted event is received.
    public virtual Task OnTestHelpersTestClockDeletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the test_helpers.test_clock.internal_failure event is received.
    public virtual Task OnTestHelpersTestClockInternalFailureAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the test_helpers.test_clock.ready event is received.
    public virtual Task OnTestHelpersTestClockReadyAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the topup.canceled event is received.
    public virtual Task OnTopupCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the topup.created event is received.
    public virtual Task OnTopupCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the topup.failed event is received.
    public virtual Task OnTopupFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the topup.reversed event is received.
    public virtual Task OnTopupReversedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the topup.succeeded event is received.
    public virtual Task OnTopupSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the transfer.created event is received.
    public virtual Task OnTransferCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the transfer.reversed event is received.
    public virtual Task OnTransferReversedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the transfer.updated event is received.
    public virtual Task OnTransferUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.credit_reversal.created event is received.
    public virtual Task OnTreasuryCreditReversalCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.credit_reversal.posted event is received.
    public virtual Task OnTreasuryCreditReversalPostedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.debit_reversal.completed event is received.
    public virtual Task OnTreasuryDebitReversalCompletedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.debit_reversal.created event is received.
    public virtual Task OnTreasuryDebitReversalCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.debit_reversal.initial_credit_granted event is received.
    public virtual Task OnTreasuryDebitReversalInitialCreditGrantedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.financial_account.closed event is received.
    public virtual Task OnTreasuryFinancialAccountClosedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.financial_account.created event is received.
    public virtual Task OnTreasuryFinancialAccountCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.financial_account.features_status_updated event is received.
    public virtual Task OnTreasuryFinancialAccountFeaturesStatusUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.inbound_transfer.canceled event is received.
    public virtual Task OnTreasuryInboundTransferCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.inbound_transfer.created event is received.
    public virtual Task OnTreasuryInboundTransferCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.inbound_transfer.failed event is received.
    public virtual Task OnTreasuryInboundTransferFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.inbound_transfer.succeeded event is received.
    public virtual Task OnTreasuryInboundTransferSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.canceled event is received.
    public virtual Task OnTreasuryOutboundPaymentCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.created event is received.
    public virtual Task OnTreasuryOutboundPaymentCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.expected_arrival_date_updated event is received.
    public virtual Task OnTreasuryOutboundPaymentExpectedArrivalDateUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.failed event is received.
    public virtual Task OnTreasuryOutboundPaymentFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.posted event is received.
    public virtual Task OnTreasuryOutboundPaymentPostedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_payment.returned event is received.
    public virtual Task OnTreasuryOutboundPaymentReturnedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.canceled event is received.
    public virtual Task OnTreasuryOutboundTransferCanceledAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.created event is received.
    public virtual Task OnTreasuryOutboundTransferCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.expected_arrival_date_updated event is received.
    public virtual Task OnTreasuryOutboundTransferExpectedArrivalDateUpdatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.failed event is received.
    public virtual Task OnTreasuryOutboundTransferFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.posted event is received.
    public virtual Task OnTreasuryOutboundTransferPostedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.outbound_transfer.returned event is received.
    public virtual Task OnTreasuryOutboundTransferReturnedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.received_credit.created event is received.
    public virtual Task OnTreasuryReceivedCreditCreatedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.received_credit.failed event is received.
    public virtual Task OnTreasuryReceivedCreditFailedAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.received_credit.succeeded event is received.
    public virtual Task OnTreasuryReceivedCreditSucceededAsync(Event e) => UnhandledEventAsync(e);

    /// Fired when the treasury.received_debit.created event is received.
    public virtual Task OnTreasuryReceivedDebitCreatedAsync(Event e) => UnhandledEventAsync(e);

    private Task ExecuteInternalAsync(Event e) => e.Type switch
    {
        "account.application.authorized" => OnAccountApplicationAuthorizedAsync(e),
        "account.application.deauthorized" => OnAccountApplicationDeauthorizedAsync(e),
        "account.external_account.created" => OnAccountExternalAccountCreatedAsync(e),
        "account.external_account.deleted" => OnAccountExternalAccountDeletedAsync(e),
        "account.external_account.updated" => OnAccountExternalAccountUpdatedAsync(e),
        "account.updated" => OnAccountUpdatedAsync(e),
        "application_fee.created" => OnApplicationFeeCreatedAsync(e),
        "application_fee.refund.updated" => OnApplicationFeeRefundUpdatedAsync(e),
        "application_fee.refunded" => OnApplicationFeeRefundedAsync(e),
        "balance.available" => OnBalanceAvailableAsync(e),
        "billing_portal.configuration.created" => OnBillingPortalConfigurationCreatedAsync(e),
        "billing_portal.configuration.updated" => OnBillingPortalConfigurationUpdatedAsync(e),
        "billing_portal.session.created" => OnBillingPortalSessionCreatedAsync(e),
        "capability.updated" => OnCapabilityUpdatedAsync(e),
        "cash_balance.funds_available" => OnCashBalanceFundsAvailableAsync(e),
        "charge.captured" => OnChargeCapturedAsync(e),
        "charge.dispute.closed" => OnChargeDisputeClosedAsync(e),
        "charge.dispute.created" => OnChargeDisputeCreatedAsync(e),
        "charge.dispute.funds_reinstated" => OnChargeDisputeFundsReinstatedAsync(e),
        "charge.dispute.funds_withdrawn" => OnChargeDisputeFundsWithdrawnAsync(e),
        "charge.dispute.updated" => OnChargeDisputeUpdatedAsync(e),
        "charge.expired" => OnChargeExpiredAsync(e),
        "charge.failed" => OnChargeFailedAsync(e),
        "charge.pending" => OnChargePendingAsync(e),
        "charge.refund.updated" => OnChargeRefundUpdatedAsync(e),
        "charge.refunded" => OnChargeRefundedAsync(e),
        "charge.succeeded" => OnChargeSucceededAsync(e),
        "charge.updated" => OnChargeUpdatedAsync(e),
        "checkout.session.async_payment_failed" => OnCheckoutSessionAsyncPaymentFailedAsync(e),
        "checkout.session.async_payment_succeeded" => OnCheckoutSessionAsyncPaymentSucceededAsync(e),
        "checkout.session.completed" => OnCheckoutSessionCompletedAsync(e),
        "checkout.session.expired" => OnCheckoutSessionExpiredAsync(e),
        "coupon.created" => OnCouponCreatedAsync(e),
        "coupon.deleted" => OnCouponDeletedAsync(e),
        "coupon.updated" => OnCouponUpdatedAsync(e),
        "credit_note.created" => OnCreditNoteCreatedAsync(e),
        "credit_note.updated" => OnCreditNoteUpdatedAsync(e),
        "credit_note.voided" => OnCreditNoteVoidedAsync(e),
        "customer.created" => OnCustomerCreatedAsync(e),
        "customer.deleted" => OnCustomerDeletedAsync(e),
        "customer.discount.created" => OnCustomerDiscountCreatedAsync(e),
        "customer.discount.deleted" => OnCustomerDiscountDeletedAsync(e),
        "customer.discount.updated" => OnCustomerDiscountUpdatedAsync(e),
        "customer.source.created" => OnCustomerSourceCreatedAsync(e),
        "customer.source.deleted" => OnCustomerSourceDeletedAsync(e),
        "customer.source.expiring" => OnCustomerSourceExpiringAsync(e),
        "customer.source.updated" => OnCustomerSourceUpdatedAsync(e),
        "customer.subscription.created" => OnCustomerSubscriptionCreatedAsync(e),
        "customer.subscription.deleted" => OnCustomerSubscriptionDeletedAsync(e),
        "customer.subscription.pending_update_applied" => OnCustomerSubscriptionPendingUpdateAppliedAsync(e),
        "customer.subscription.pending_update_expired" => OnCustomerSubscriptionPendingUpdateExpiredAsync(e),
        "customer.subscription.trial_will_end" => OnCustomerSubscriptionTrialWillEndAsync(e),
        "customer.subscription.updated" => OnCustomerSubscriptionUpdatedAsync(e),
        "customer.tax_id.created" => OnCustomerTaxIdCreatedAsync(e),
        "customer.tax_id.deleted" => OnCustomerTaxIdDeletedAsync(e),
        "customer.tax_id.updated" => OnCustomerTaxIdUpdatedAsync(e),
        "customer.updated" => OnCustomerUpdatedAsync(e),
        "customer_cash_balance_transaction.created" => OnCustomerCashBalanceTransactionCreatedAsync(e),
        "file.created" => OnFileCreatedAsync(e),
        "financial_connections.account.created" => OnFinancialConnectionsAccountCreatedAsync(e),
        "financial_connections.account.deactivated" => OnFinancialConnectionsAccountDeactivatedAsync(e),
        "financial_connections.account.disconnected" => OnFinancialConnectionsAccountDisconnectedAsync(e),
        "financial_connections.account.reactivated" => OnFinancialConnectionsAccountReactivatedAsync(e),
        "financial_connections.account.refreshed_balance" => OnFinancialConnectionsAccountRefreshedBalanceAsync(e),
        "identity.verification_session.canceled" => OnIdentityVerificationSessionCanceledAsync(e),
        "identity.verification_session.created" => OnIdentityVerificationSessionCreatedAsync(e),
        "identity.verification_session.processing" => OnIdentityVerificationSessionProcessingAsync(e),
        "identity.verification_session.redacted" => OnIdentityVerificationSessionRedactedAsync(e),
        "identity.verification_session.requires_input" => OnIdentityVerificationSessionRequiresInputAsync(e),
        "identity.verification_session.verified" => OnIdentityVerificationSessionVerifiedAsync(e),
        "invoice.created" => OnInvoiceCreatedAsync(e),
        "invoice.deleted" => OnInvoiceDeletedAsync(e),
        "invoice.finalization_failed" => OnInvoiceFinalizationFailedAsync(e),
        "invoice.finalized" => OnInvoiceFinalizedAsync(e),
        "invoice.marked_uncollectible" => OnInvoiceMarkedUncollectibleAsync(e),
        "invoice.paid" => OnInvoicePaidAsync(e),
        "invoice.payment_action_required" => OnInvoicePaymentActionRequiredAsync(e),
        "invoice.payment_failed" => OnInvoicePaymentFailedAsync(e),
        "invoice.payment_succeeded" => OnInvoicePaymentSucceededAsync(e),
        "invoice.sent" => OnInvoiceSentAsync(e),
        "invoice.upcoming" => OnInvoiceUpcomingAsync(e),
        "invoice.updated" => OnInvoiceUpdatedAsync(e),
        "invoice.voided" => OnInvoiceVoidedAsync(e),
        "invoiceitem.created" => OnInvoiceitemCreatedAsync(e),
        "invoiceitem.deleted" => OnInvoiceitemDeletedAsync(e),
        "invoiceitem.updated" => OnInvoiceitemUpdatedAsync(e),
        "issuing_authorization.created" => OnIssuingAuthorizationCreatedAsync(e),
        "issuing_authorization.request" => OnIssuingAuthorizationRequestAsync(e),
        "issuing_authorization.updated" => OnIssuingAuthorizationUpdatedAsync(e),
        "issuing_card.created" => OnIssuingCardCreatedAsync(e),
        "issuing_card.updated" => OnIssuingCardUpdatedAsync(e),
        "issuing_cardholder.created" => OnIssuingCardholderCreatedAsync(e),
        "issuing_cardholder.updated" => OnIssuingCardholderUpdatedAsync(e),
        "issuing_dispute.closed" => OnIssuingDisputeClosedAsync(e),
        "issuing_dispute.created" => OnIssuingDisputeCreatedAsync(e),
        "issuing_dispute.funds_reinstated" => OnIssuingDisputeFundsReinstatedAsync(e),
        "issuing_dispute.submitted" => OnIssuingDisputeSubmittedAsync(e),
        "issuing_dispute.updated" => OnIssuingDisputeUpdatedAsync(e),
        "issuing_transaction.created" => OnIssuingTransactionCreatedAsync(e),
        "issuing_transaction.updated" => OnIssuingTransactionUpdatedAsync(e),
        "mandate.updated" => OnMandateUpdatedAsync(e),
        "order.created" => OnOrderCreatedAsync(e),
        "payment_intent.amount_capturable_updated" => OnPaymentIntentAmountCapturableUpdatedAsync(e),
        "payment_intent.canceled" => OnPaymentIntentCanceledAsync(e),
        "payment_intent.created" => OnPaymentIntentCreatedAsync(e),
        "payment_intent.partially_funded" => OnPaymentIntentPartiallyFundedAsync(e),
        "payment_intent.payment_failed" => OnPaymentIntentPaymentFailedAsync(e),
        "payment_intent.processing" => OnPaymentIntentProcessingAsync(e),
        "payment_intent.requires_action" => OnPaymentIntentRequiresActionAsync(e),
        "payment_intent.succeeded" => OnPaymentIntentSucceededAsync(e),
        "payment_link.created" => OnPaymentLinkCreatedAsync(e),
        "payment_link.updated" => OnPaymentLinkUpdatedAsync(e),
        "payment_method.attached" => OnPaymentMethodAttachedAsync(e),
        "payment_method.automatically_updated" => OnPaymentMethodAutomaticallyUpdatedAsync(e),
        "payment_method.detached" => OnPaymentMethodDetachedAsync(e),
        "payment_method.updated" => OnPaymentMethodUpdatedAsync(e),
        "payout.canceled" => OnPayoutCanceledAsync(e),
        "payout.created" => OnPayoutCreatedAsync(e),
        "payout.failed" => OnPayoutFailedAsync(e),
        "payout.paid" => OnPayoutPaidAsync(e),
        "payout.updated" => OnPayoutUpdatedAsync(e),
        "person.created" => OnPersonCreatedAsync(e),
        "person.deleted" => OnPersonDeletedAsync(e),
        "person.updated" => OnPersonUpdatedAsync(e),
        "plan.created" => OnPlanCreatedAsync(e),
        "plan.deleted" => OnPlanDeletedAsync(e),
        "plan.updated" => OnPlanUpdatedAsync(e),
        "price.created" => OnPriceCreatedAsync(e),
        "price.deleted" => OnPriceDeletedAsync(e),
        "price.updated" => OnPriceUpdatedAsync(e),
        "product.created" => OnProductCreatedAsync(e),
        "product.deleted" => OnProductDeletedAsync(e),
        "product.updated" => OnProductUpdatedAsync(e),
        "promotion_code.created" => OnPromotionCodeCreatedAsync(e),
        "promotion_code.updated" => OnPromotionCodeUpdatedAsync(e),
        "quote.accepted" => OnQuoteAcceptedAsync(e),
        "quote.canceled" => OnQuoteCanceledAsync(e),
        "quote.created" => OnQuoteCreatedAsync(e),
        "quote.finalized" => OnQuoteFinalizedAsync(e),
        "radar.early_fraud_warning.created" => OnRadarEarlyFraudWarningCreatedAsync(e),
        "radar.early_fraud_warning.updated" => OnRadarEarlyFraudWarningUpdatedAsync(e),
        "recipient.created" => OnRecipientCreatedAsync(e),
        "recipient.deleted" => OnRecipientDeletedAsync(e),
        "recipient.updated" => OnRecipientUpdatedAsync(e),
        "reporting.report_run.failed" => OnReportingReportRunFailedAsync(e),
        "reporting.report_run.succeeded" => OnReportingReportRunSucceededAsync(e),
        "reporting.report_type.updated" => OnReportingReportTypeUpdatedAsync(e),
        "review.closed" => OnReviewClosedAsync(e),
        "review.opened" => OnReviewOpenedAsync(e),
        "setup_intent.canceled" => OnSetupIntentCanceledAsync(e),
        "setup_intent.created" => OnSetupIntentCreatedAsync(e),
        "setup_intent.requires_action" => OnSetupIntentRequiresActionAsync(e),
        "setup_intent.setup_failed" => OnSetupIntentSetupFailedAsync(e),
        "setup_intent.succeeded" => OnSetupIntentSucceededAsync(e),
        "sigma.scheduled_query_run.created" => OnSigmaScheduledQueryRunCreatedAsync(e),
        "sku.created" => OnSkuCreatedAsync(e),
        "sku.deleted" => OnSkuDeletedAsync(e),
        "sku.updated" => OnSkuUpdatedAsync(e),
        "source.canceled" => OnSourceCanceledAsync(e),
        "source.chargeable" => OnSourceChargeableAsync(e),
        "source.failed" => OnSourceFailedAsync(e),
        "source.mandate_notification" => OnSourceMandateNotificationAsync(e),
        "source.refund_attributes_required" => OnSourceRefundAttributesRequiredAsync(e),
        "source.transaction.created" => OnSourceTransactionCreatedAsync(e),
        "source.transaction.updated" => OnSourceTransactionUpdatedAsync(e),
        "subscription_schedule.aborted" => OnSubscriptionScheduleAbortedAsync(e),
        "subscription_schedule.canceled" => OnSubscriptionScheduleCanceledAsync(e),
        "subscription_schedule.completed" => OnSubscriptionScheduleCompletedAsync(e),
        "subscription_schedule.created" => OnSubscriptionScheduleCreatedAsync(e),
        "subscription_schedule.expiring" => OnSubscriptionScheduleExpiringAsync(e),
        "subscription_schedule.released" => OnSubscriptionScheduleReleasedAsync(e),
        "subscription_schedule.updated" => OnSubscriptionScheduleUpdatedAsync(e),
        "tax_rate.created" => OnTaxRateCreatedAsync(e),
        "tax_rate.updated" => OnTaxRateUpdatedAsync(e),
        "terminal.reader.action_failed" => OnTerminalReaderActionFailedAsync(e),
        "terminal.reader.action_succeeded" => OnTerminalReaderActionSucceededAsync(e),
        "test_helpers.test_clock.advancing" => OnTestHelpersTestClockAdvancingAsync(e),
        "test_helpers.test_clock.created" => OnTestHelpersTestClockCreatedAsync(e),
        "test_helpers.test_clock.deleted" => OnTestHelpersTestClockDeletedAsync(e),
        "test_helpers.test_clock.internal_failure" => OnTestHelpersTestClockInternalFailureAsync(e),
        "test_helpers.test_clock.ready" => OnTestHelpersTestClockReadyAsync(e),
        "topup.canceled" => OnTopupCanceledAsync(e),
        "topup.created" => OnTopupCreatedAsync(e),
        "topup.failed" => OnTopupFailedAsync(e),
        "topup.reversed" => OnTopupReversedAsync(e),
        "topup.succeeded" => OnTopupSucceededAsync(e),
        "transfer.created" => OnTransferCreatedAsync(e),
        "transfer.reversed" => OnTransferReversedAsync(e),
        "transfer.updated" => OnTransferUpdatedAsync(e),
        "treasury.credit_reversal.created" => OnTreasuryCreditReversalCreatedAsync(e),
        "treasury.credit_reversal.posted" => OnTreasuryCreditReversalPostedAsync(e),
        "treasury.debit_reversal.completed" => OnTreasuryDebitReversalCompletedAsync(e),
        "treasury.debit_reversal.created" => OnTreasuryDebitReversalCreatedAsync(e),
        "treasury.debit_reversal.initial_credit_granted" => OnTreasuryDebitReversalInitialCreditGrantedAsync(e),
        "treasury.financial_account.closed" => OnTreasuryFinancialAccountClosedAsync(e),
        "treasury.financial_account.created" => OnTreasuryFinancialAccountCreatedAsync(e),
        "treasury.financial_account.features_status_updated" => OnTreasuryFinancialAccountFeaturesStatusUpdatedAsync(e),
        "treasury.inbound_transfer.canceled" => OnTreasuryInboundTransferCanceledAsync(e),
        "treasury.inbound_transfer.created" => OnTreasuryInboundTransferCreatedAsync(e),
        "treasury.inbound_transfer.failed" => OnTreasuryInboundTransferFailedAsync(e),
        "treasury.inbound_transfer.succeeded" => OnTreasuryInboundTransferSucceededAsync(e),
        "treasury.outbound_payment.canceled" => OnTreasuryOutboundPaymentCanceledAsync(e),
        "treasury.outbound_payment.created" => OnTreasuryOutboundPaymentCreatedAsync(e),
        "treasury.outbound_payment.expected_arrival_date_updated" => OnTreasuryOutboundPaymentExpectedArrivalDateUpdatedAsync(e),
        "treasury.outbound_payment.failed" => OnTreasuryOutboundPaymentFailedAsync(e),
        "treasury.outbound_payment.posted" => OnTreasuryOutboundPaymentPostedAsync(e),
        "treasury.outbound_payment.returned" => OnTreasuryOutboundPaymentReturnedAsync(e),
        "treasury.outbound_transfer.canceled" => OnTreasuryOutboundTransferCanceledAsync(e),
        "treasury.outbound_transfer.created" => OnTreasuryOutboundTransferCreatedAsync(e),
        "treasury.outbound_transfer.expected_arrival_date_updated" => OnTreasuryOutboundTransferExpectedArrivalDateUpdatedAsync(e),
        "treasury.outbound_transfer.failed" => OnTreasuryOutboundTransferFailedAsync(e),
        "treasury.outbound_transfer.posted" => OnTreasuryOutboundTransferPostedAsync(e),
        "treasury.outbound_transfer.returned" => OnTreasuryOutboundTransferReturnedAsync(e),
        "treasury.received_credit.created" => OnTreasuryReceivedCreditCreatedAsync(e),
        "treasury.received_credit.failed" => OnTreasuryReceivedCreditFailedAsync(e),
        "treasury.received_credit.succeeded" => OnTreasuryReceivedCreditSucceededAsync(e),
        "treasury.received_debit.created" => OnTreasuryReceivedDebitCreatedAsync(e),
        _ => UnknownEventAsync(e),
    };
}
