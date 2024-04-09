using Microsoft.Extensions.Logging;

namespace CloudflareDnsync.Services;

public sealed class IPService(ILogger<IPService> logger) : IIPService
{
    private readonly Uri[] _providers = [
        new Uri("https://api.ipify.org"),
        new Uri("https://icanhazip.com"),
        new Uri("https://ifconfig.me"),
        new Uri("https://ident.me"),
    ];

    public async Task<string> GetPublicIpAsync(CancellationToken cancellationToken = default)
    {
        foreach (var provider in _providers)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(provider, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to retrieve public IP from {Provider}", provider.Host);
            }
        }
        throw new Exception("Failed to retrieve public IP from all providers");
    }
}
