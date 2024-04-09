namespace CloudflareDnsync.Services;

public interface IIPService
{
    Task<string> GetPublicIpAsync(CancellationToken cancellationToken = default);
}
