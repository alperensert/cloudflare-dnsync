using System.Text;
using CloudflareDnsync.Models;
using CloudflareDnsync.Models.Responses;
using Newtonsoft.Json;

namespace CloudflareDnsync.Services;

public class CloudflareService : ICloudflareService
{
    private bool _disposed;

    private const string BaseUrl = "https://api.cloudflare.com/client/v4/";

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(BaseUrl)
    };

    public CloudflareService(string apiKey, string apiEmail, string? baseAddress = null)
    {
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Key", apiKey);
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Email", apiEmail);
        if (baseAddress is not null)
            _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public CloudflareService(string token, string? baseAddress = null)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        if (baseAddress is not null)
            _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public Task<CloudflareResponse<List<Zone>>> GetZonesAsync(
        int page = 1,
        int perPage = 20,
        CancellationToken cancellationToken = default)
        => SendAsync<CloudflareResponse<List<Zone>>>(
            HttpMethod.Get,
            $"zones?page={page}&per_page={perPage}",
            cancellationToken: cancellationToken);

    public Task<CloudflareResponse<TokenVerify>> VerifyTokenAsync(CancellationToken cancellationToken = default)
        => SendAsync<CloudflareResponse<TokenVerify>>(
            HttpMethod.Get,
            "user/tokens/verify",
            cancellationToken: cancellationToken);

    public Task<CloudflareResponse<List<DnsRecord>>> GetDnsRecordsAsync(
        string zoneId,
        int page = 1,
        int perPage = 20,
        CancellationToken cancellationToken = default)
        => SendAsync<CloudflareResponse<List<DnsRecord>>>(
            HttpMethod.Get,
            $"zones/{zoneId}/dns_records?page={page}&per_page={perPage}&type=A",
            cancellationToken: cancellationToken);

    public Task<CloudflareResponse<DnsRecord>> GetDnsRecordAsync(
        string zoneId,
        string recordId,
        CancellationToken cancellationToken = default)
        => SendAsync<CloudflareResponse<DnsRecord>>(
            HttpMethod.Get,
            $"zones/{zoneId}/dns_records/{recordId}",
            cancellationToken: cancellationToken);

    public Task<CloudflareResponse<DnsRecord>> UpdateDnsRecordAsync(
        string zoneId,
        string recordId,
        DnsRecordUpdateRequest request,
        CancellationToken cancellationToken = default)
        => SendAsync<CloudflareResponse<DnsRecord>>(
            HttpMethod.Patch,
            $"zones/{zoneId}/dns_records/{recordId}",
            request,
            ignoreNullValues: true,
            cancellationToken: cancellationToken);

    /// <summary>
    /// Sends an HTTP request to the specified URL and returns the response as a deserialized object.
    /// </summary>
    /// <typeparam name="TResult">Result will deserialized to this type.</typeparam>
    /// <param name="method">Http method to sending http request.</param>
    /// <param name="url">Request URL.</param>
    /// <param name="data">Optional request data, it will be sent as UTF-8 and "application/json".</param>
    /// <param name="ignoreNullValues">If true, null values will be ignored while serializing the data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// Deserialized object of the response.
    /// </returns>
    private async Task<TResult> SendAsync<TResult>(
        HttpMethod method,
        string url,
        object? data = null,
        bool? ignoreNullValues = null,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(method, url);
        if (data is not null && (method == HttpMethod.Patch || method == HttpMethod.Post || method == HttpMethod.Put))
        {
            string? json;
            if (ignoreNullValues is true)
                json = JsonConvert.SerializeObject(
                    data,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            else
                json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        var response = await _httpClient.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<TResult>(content)!;
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        _httpClient.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~CloudflareService() => Dispose();
}
