using Newtonsoft.Json;

namespace CloudflareDnsync.Models;

public class Zone
{
    [JsonProperty("activated_on")]
    public DateTime ActivatedOn { get; init; }

    [JsonProperty("created_on")]
    public DateTime CreatedOn { get; init; }

    [JsonProperty("development_mode")]
    public int DevelopmentMode { get; init; }

    [JsonProperty("modified_on")]
    public DateTime ModifiedOn { get; init; }

    [JsonProperty("id")]
    public string Id { get; init; } = null!;

    [JsonProperty("name")]
    public string Name { get; init; } = null!;

    [JsonProperty("original_name_servers")]
    public string[] OriginalNameServers { get; init; } = null!;

    [JsonProperty("original_registrar")]
    public string OriginalRegistrar { get; init; } = null!;

    [JsonProperty("original_dnshost")]
    public string OriginalDnsHost { get; init; } = null!;

    [JsonProperty("owner")]
    public Zone_Owner Owner { get; init; } = null!;

    [JsonProperty("account")]
    public Zone_Account Account { get; init; } = null!;

    public class Zone_Owner
    {
        [JsonProperty("id")]
        public string Id { get; init; } = null!;

        [JsonProperty("name")]
        public string Name { get; init; } = null!;

        [JsonProperty("type")]
        public string Type { get; init; } = null!;
    }

    public class Zone_Account
    {
        [JsonProperty("id")]
        public string Id { get; init; } = null!;

        [JsonProperty("name")]
        public string Name { get; init; } = null!;
    }

    public class Zone_Meta
    {
        [JsonProperty("cdn_only")]
        public bool CdnOnly { get; init; }

        [JsonProperty("custom_certificate_quota")]
        public int CustomCertificateQuota { get; init; }

        [JsonProperty("dns_only")]
        public bool DnsOnly { get; init; }

        [JsonProperty("page_rule_quota")]
        public int PageRuleQuota { get; init; }

        [JsonProperty("phishing_detected")]
        public bool PhishingDetected { get; init; }

        [JsonProperty("foundation_dns")]
        public bool FoundationDns { get; init; }

        [JsonProperty("step")]
        public int Step { get; init; }
    }
}
