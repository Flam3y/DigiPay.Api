namespace DigiPay.Api.Requests;

internal class GetPaymentRequest() : RequestBase("v3/fiat/payments/get")
{
    public string? PaymentId { get; set; }
    public string? PartnerPaymentId { get; set; }
}
