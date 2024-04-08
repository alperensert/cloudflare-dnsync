using Newtonsoft.Json;

namespace CloudflareDnsync.Models;

public sealed class DnsRecordUpdateRequest(string type, string name, string content)
{
    [JsonProperty("type")]
    public string Type { get; init; } = type;

    [JsonProperty("name")]
    public string Name { get; init; } = name;

    [JsonProperty("content")]
    public string Content { get; init; } = content;

    [JsonProperty("ttl")]
    public int? Ttl { get; set; }

    [JsonProperty("proxied")]
    public bool? Proxied { get; set; }

    [JsonProperty("comment")]
    public string? Comment { get; set; }
}
