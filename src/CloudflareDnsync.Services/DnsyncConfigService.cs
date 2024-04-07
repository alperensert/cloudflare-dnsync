using CloudflareDnsync.Models;
using Newtonsoft.Json;

namespace CloudflareDnsync.Services;

public sealed class DnsyncConfigService : IDnsyncConfigService
{
    private const string ConfigurationFile = ".dnsync.json";

    private readonly string _configPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ConfigurationFile);

    private readonly List<DnsyncConfiguration> _configurations = [];

    public DnsyncConfigService()
    {
        if (!IsConfigExists)
        {
            var configCreationTask = CreateConfigAsync();
            configCreationTask.Wait();
        }
        var configRetrievingTask = RetrieveConfigAsync();
        configRetrievingTask.Wait();
        _configurations = configRetrievingTask.Result;
    }

    public IEnumerable<DnsyncConfiguration> GetAll()
        => _configurations;

    public DnsyncConfiguration? GetById(string id)
        => _configurations.FirstOrDefault(c => c.ZoneId == id);

    public DnsyncConfiguration? GetByName(string name)
        => _configurations.FirstOrDefault(c => c.Name == name);

    public Task AddAsync(DnsyncConfiguration configuration, CancellationToken cancellationToken = default)
    {
        _configurations.Add(configuration);
        return SaveAsync(cancellationToken);
    }

    public Task RemoveAsync(DnsyncConfiguration configuration, CancellationToken cancellationToken = default)
    {
        _configurations.Remove(configuration);
        return SaveAsync(cancellationToken);
    }

    public Task RemoveByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var configuration = _configurations.FirstOrDefault(c => c.Name == name);
        if (configuration is not null)
            return RemoveAsync(configuration, cancellationToken);
        return Task.CompletedTask;
    }

    private Task SaveAsync(CancellationToken cancellationToken = default)
    {
        var json = JsonConvert.SerializeObject(_configurations, Formatting.Indented);
        return File.WriteAllTextAsync(_configPath, json, cancellationToken);
    }

    private async Task<List<DnsyncConfiguration>> RetrieveConfigAsync(CancellationToken cancellationToken = default)
    {
        var json = await File.ReadAllTextAsync(_configPath, cancellationToken);
        return JsonConvert.DeserializeObject<List<DnsyncConfiguration>>(json)!;
    }

    private Task CreateConfigAsync(CancellationToken cancellationToken = default)
    {
        List<DnsyncConfiguration> configurations = [];
        var json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
        return File.WriteAllTextAsync(_configPath, json, cancellationToken);
    }

    private bool IsConfigExists => File.Exists(_configPath);
}
