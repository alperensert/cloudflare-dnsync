using Newtonsoft.Json;

namespace CloudflareDnsync.Models;

public class TokenVerify
{
    [JsonProperty("id")]
    public string Id { get; init; } = null!;

    [JsonProperty("status")]
    public string Status { get; init; } = null!;

    [JsonProperty("not_before")]
    public DateTime NotBefore { get; init; }

    [JsonProperty("expires_on")]
    public DateTime ExpiresOn { get; init; }

    [JsonIgnore]
    public bool IsValid => Status == "active";
}
