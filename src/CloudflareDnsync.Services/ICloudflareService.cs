using CloudflareDnsync.Models;
using CloudflareDnsync.Models.Responses;

namespace CloudflareDnsync.Services;

/// <summary>
/// Represents a service for interacting with the Cloudflare API.
/// </summary>
public interface ICloudflareService : IDisposable
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

    /// <summary>
    /// Retrieves a DNS record for a zone from Cloudflare.
    /// </summary>
    /// <param name="zoneId">The ID of the dns record's zone.</param>
    /// <param name="recordId">The ID of the dns record.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the DNS record.
    /// </returns>
    Task<CloudflareResponse<DnsRecord>> GetDnsRecordAsync(
        string zoneId,
        string recordId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a DNS record for a zone in Cloudflare.
    /// </summary>
    /// <param name="zoneId">The ID of the zone to update the DNS record for.</param>
    /// <param name="recordId">The ID of the dns record</param>
    /// <param name="request">The request containing the updated DNS record data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the updated DNS record.
    /// </returns>
    Task<CloudflareResponse<DnsRecord>> UpdateDnsRecordAsync(
        string zoneId,
        string recordId,
        DnsRecordUpdateRequest request,
        CancellationToken cancellationToken = default);
}
