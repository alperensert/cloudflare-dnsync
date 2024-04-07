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
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the list of zones.
    /// </returns>
    Task<CloudflareResponse<List<Zone>>> GetZonesAsync(int page = 1, int perPage = 20);

    /// <summary>
    /// Verifies the Cloudflare API token.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the Cloudflare response containing the token verification result.
    /// </returns>
    Task<CloudflareResponse<TokenVerify>> VerifyTokenAsync();
}
