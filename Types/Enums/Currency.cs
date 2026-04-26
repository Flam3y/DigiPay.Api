using DigiPay.Api.Serialization;
using System.Text.Json.Serialization;

namespace DigiPay.Api.Types.Enums;

[JsonConverter(typeof(ToUpperConverter<Currency>))]
public enum Currency
{
    Myr,
    Kzt,
    Azn,
    Uzs,
    Cop,
    Brl,
    Ars,
    Rub,
    Ngn,
    Xof,
    Tzs,
    Ugx,
    Cdf,
    Xaf,
    Ghs,
    Kes,
    Zar,
    Jpy,
    Eur,
    Cny,
    Try,
    Mad,
    Inr,
    Egp,
    Thb,
    Krw,
    Bdt,
    Sgd,
    Hkd,
    Php,
    Idr,
    Vnd
}
