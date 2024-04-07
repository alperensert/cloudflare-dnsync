using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace CloudflareDnsync.Models;

public sealed class DnsyncConfiguration
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("api_key")]
    public string? ApiKey { get; set; }

    [JsonProperty("api_email")]
    public string? ApiEmail { get; set; }

    [JsonProperty("token")]
    public string? Token { get; set; }

    [JsonProperty("record_id")]
    public string Id { get; set; } = null!;

    [JsonProperty("zone_id")]
    public string ZoneId { get; set; } = null!;

    [JsonProperty("proxy")]
    public bool UseProxy { get; set; }

    [JsonProperty("record_type")]
    public RecordType Type { get; set; }

    [JsonIgnore]
    [MemberNotNullWhen(false, nameof(ApiKey))]
    [MemberNotNullWhen(false, nameof(ApiEmail))]
    [MemberNotNullWhen(true, nameof(Token))]
    public bool IsUsingToken => !string.IsNullOrWhiteSpace(Token);

    public enum RecordType
    {
        A,
        AAAA
    }
}
