using CloudflareDnsync.Models;
using CloudflareDnsync.Models.Responses;

namespace CloudflareDnsync.Services;

public interface ICloudflareService
{
    Task<CloudflareResponse<List<Zone>>> GetZonesAsync(int page = 1, int perPage = 20);

    Task<CloudflareResponse<TokenVerify>> VerifyTokenAsync();
}
