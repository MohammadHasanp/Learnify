using Newtonsoft.Json;

namespace TransactionModule.Services.Zarinpal.DTOs.Refound;

public class ReFoundRequest
{
    [JsonProperty("merchant_id")]
    public string MerchantId { get; set; } = null!;

    [JsonProperty("authority")]
    public string Authority { get; set; } = null!;

}

public class ReFoundResponse
{
    public List<FinalReFoundResponse> Data { get; set; } = null!;
    public ReFoundErrorResponse Errors { get; set; } = null!;
}

public class FinalReFoundResponse
{
    public string code { get; set; } = null!;
    public string message { get; set; } = null!;
    public int ref_id { get; set; }
    public int session { get; set; }
    public string iban { get; set; } = null!;
}

public class ReFoundErrorResponse
{
    public string message { get; set; } = null!;
    public int code { get; set; }
}