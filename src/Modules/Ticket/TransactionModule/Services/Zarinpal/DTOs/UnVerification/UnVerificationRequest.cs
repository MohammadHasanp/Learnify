using Newtonsoft.Json;

namespace TransactionModule.Services.Zarinpal.DTOs.UnVerification;

public class UnVerificationRequest
{
    [JsonProperty("merchant_id")]
    public string MerchantId { get; set; } = null!;
}

public class UnVerificationResponse
{
    [JsonProperty("data")]
    public UnVerificationFinallyResponse Data { get; set; }
    public List<object> errors { get; set; }
}

public class UnVerificationFinallyResponse
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("authorities")]
    public List<UnVerificationResponseItem> Authorities { get; set; }
}

public class UnVerificationResponseItem
{
    [JsonProperty("authority")]
    public string Authority { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("callback_url")]
    public string CallbackUrl { get; set; }

    [JsonProperty("referer")]
    public string Referer { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }
}