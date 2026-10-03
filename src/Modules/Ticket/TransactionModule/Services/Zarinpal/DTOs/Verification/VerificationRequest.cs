using Newtonsoft.Json;

namespace TransactionModule.Services.Zarinpal.DTOs.Verification;

public class VerificationRequest
{
    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("merchant_id")]
    public string MerchantId { get; set; }

    [JsonProperty("authority")]
    public string Authority { get; set; }
}

public class VerificationResponse
{
    [JsonProperty("data")]
    public VerificationResponseData Data { get; set; }
    [JsonProperty("errors")]
    public List<VerificationResponseError> Errors { get; set; } = [];
}
public class ErrorVerificationResponse
{
    [JsonProperty("data")]
    public List<VerificationResponseData> Data { get; set; } = [];
    [JsonProperty("errors")]
    public VerificationResponseError Errors { get; set; }
}
public class SandBoxVerificationResponse
{
    public long RefId { get; set; }
    public int Status { get; set; }
}
public class VerificationResponseData
{
    [JsonProperty("code")]
    public int Status { get; set; }

    [JsonProperty("ref_id")]
    public long RefId { get; set; }

    [JsonProperty("card_pan")]
    public string CardPan { get; set; }

    [JsonProperty("fee_type")]
    public string FeeType { get; set; }

    [JsonProperty("fee")]
    public int Fee { get; set; }
}

public class FinallyVerificationResponse
{
    [JsonProperty("code")]
    public int Status { get; set; }

    [JsonProperty("ref_id")]
    public long RefId { get; set; }

    [JsonProperty("card_pan")]
    public string? CardPan { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}
public class VerificationResponseError
{
    public int Code { get; set; }
    public string Message { get; set; }
}