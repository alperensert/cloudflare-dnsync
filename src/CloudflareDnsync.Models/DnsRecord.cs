using Newtonsoft.Json;

namespace CloudflareDnsync.Models;

public sealed class DnsRecord
{
    [JsonProperty("content")]
    public string Content { get; init; } = null!;

    [JsonProperty("name")]
    public string Name { get; init; } = null!;

    [JsonProperty("proxied")]
    public bool Proxied { get; init; }

    [JsonProperty("type")]
    public string Type { get; init; } = null!;

    [JsonProperty("comment")]
    public string? Comment { get; init; }

    [JsonProperty("id")]
    public string Id { get; init; } = null!;

    [JsonProperty("locked")]
    public bool Locked { get; init; }

    [JsonProperty("modified_on")]
    public DateTime ModifiedOn { get; init; }

    [JsonProperty("proxiable")]
    public bool Proxiable { get; init; }

    [JsonProperty("tags")]
    public List<string> Tags { get; init; } = [];

    [JsonProperty("zone_id")]
    public string ZoneId { get; init; } = null!;

    [JsonProperty("zone_name")]
    public string ZoneName { get; init; } = null!;

    [JsonProperty("ttl")]
    public int Ttl { get; init; }

    [JsonProperty("meta")]
    public DnsRecord_Meta? Meta { get; init; }

    public class DnsRecord_Meta
    {
        [JsonProperty("auto_added")]
        public bool AutoAdded { get; init; }

        [JsonProperty("source")]
        public string? Source { get; init; }
    }
}
