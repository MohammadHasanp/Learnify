using Newtonsoft.Json;

namespace TransactionModule.Services.Zarinpal.DTOs.Payment;

public class PaymentRequest
{
    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("callback_url")]
    public string CallbackUrl { get; set; } = null!;

    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("merchant_id")]
    public string MerchantId { get; set; } = null!;
}

public class PaymentResponse
{
    public PaymentResponseData Data { get; set; } = null!;
}

public class PaymentResponseData
{
    [JsonProperty("code")]
    public int Status { get; set; }

    [JsonProperty("authority")]
    public string Authority { get; set; } = null!;

    [JsonProperty("fee")]
    public int Fee { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = null!;

    public string GateWayUrl { get; set; } = null!;
}

public class SandBoxPaymentResponse
{
    public int Status { get; set; }
    public string Authority { get; set; } = null!;
}