using DigiPay.Api.Types.Enums;

namespace DigiPay.Api.Requests.InPayments;

internal class CreateNSPKInPaymentRequest() : RequestBase("v3/fiat/payments/create")
{
    public required string PartnerPaymentId { get; set; }

    public required int Amount { get; set; }

    public required Currency Currency { get; set; }

    public PaymentType PaymentType { get; } = PaymentType.Acq_SBP;

    public required Uri CallbackUrl { get; set; }

    public required Uri BackUrl { get; set; }

    public required int PaymentLifeTime { get; set; }

    public string? Payload { get; set; }
}
