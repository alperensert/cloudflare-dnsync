using CloudflareDnsync.Abstractions;
using CloudflareDnsync.Models;
using Newtonsoft.Json;

namespace CloudflareDnsync.Services;

public sealed class DnsyncConfigService : IDnsyncConfigService
{
    private const string ConfigurationFile = ".dnsync.json";

    private readonly string _configPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ConfigurationFile);

    public DnsyncConfigService()
    {
        if (!IsConfigExists)
        {
            var configCreationTask = CreateConfigAsync();
            configCreationTask.Wait();
        }
    }

    private Task CreateConfigAsync()
    {
        List<DnsyConfiguration> configurations = [];
        var json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
        return File.WriteAllTextAsync(_configPath, json);
    }

    private bool IsConfigExists => File.Exists(_configPath);
}
