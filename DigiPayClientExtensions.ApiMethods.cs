using DigiPay.Api.Requests;
using DigiPay.Api.Requests.InPayments;
using DigiPay.Api.Types;
using DigiPay.Api.Types.Enums;

namespace DigiPay.Api;

public static partial class DigiPayClientExtensions
{
    public static async Task<Payment<NSPKCredentials>> CreateNSPKInPayment(
        this IDigiPayClient apiClient,
        string partnerPaymentId,
        int amount,
        Currency currency,
        Uri callbackUrl,
        Uri backUrl,
        int paymentLifeTime,
        string? payload = default,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.SendRequest<Payment<NSPKCredentials>>(
            new CreateNSPKInPaymentRequest()
            {
                PartnerPaymentId = partnerPaymentId,
                Amount = amount,
                Currency = currency,
                CallbackUrl = callbackUrl,
                BackUrl = backUrl,
                PaymentLifeTime = paymentLifeTime,
                Payload = payload,
            }, cancellationToken);
    }

    public static async Task<Payment<object>> GetPayment(
        this IDigiPayClient apiClient,
        string? paymentId = default,
        string? partnerPaymentId = default,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.SendRequest<Payment<object>>(
            new GetPaymentRequest()
            {
                PaymentId = paymentId,
                PartnerPaymentId = partnerPaymentId,
            }, cancellationToken);
    }
}
