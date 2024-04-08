using CloudflareDnsync.Models;
using CloudflareDnsync.Models.Responses;

namespace CloudflareDnsync.Services;

/// <summary>
/// Represents a service for interacting with the Cloudflare API.
/// </summary>
public interface ICloudflareService
{
    /// <summary>
    /// Retrieves a list of zones from Cloudflare.
    /// </summary>
    /// <param name="page">The page number of the results to retrieve.</param>
    /// <param name="perPage">The number of results per page.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the list of zones.
    /// </returns>
    Task<CloudflareResponse<List<Zone>>> GetZonesAsync(
        int page = 1,
        int perPage = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies the Cloudflare API token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the token verification result.
    /// </returns>
    Task<CloudflareResponse<TokenVerify>> VerifyTokenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of DNS records for a zone from Cloudflare.
    /// </summary>
    /// <param name="zoneId">The ID of the zone to retrieve the DNS records for.</param>
    /// <param name="page">The page number of the results to retrieve.</param>
    /// <param name="perPage">The number of results per page.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the list of DNS records.
    /// </returns>
    Task<CloudflareResponse<List<DnsRecord>>> GetDnsRecordsAsync(
        string zoneId,
        int page = 1,
        int perPage = 20,
        CancellationToken cancellationToken = default);
}
