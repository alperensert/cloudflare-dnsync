using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace CloudflareDnsync.Models.Responses;

public class CloudflareResponse<T>
{
    [MemberNotNullWhen(true, nameof(Result))]
    [JsonProperty("success")]
    public bool Success { get; init; }

    [JsonProperty("errors")]
    public Error[] Errors { get; init; } = null!;

    [JsonProperty("messages")]
    public Message[] Messages { get; init; } = null!;

    [JsonProperty("result")]
    public T? Result { get; init; }

    [JsonProperty("result_info")]
    public ResultInformation? ResultInfo { get; init; }

    public class Error
    {
        public int Code { get; init; }

        public string Message { get; init; } = null!;
    }

    public class Message : Error
    {
    }

    public class ResultInformation
    {
        [JsonProperty("page")]
        public string Page { get; init; } = null!;

        [JsonProperty("per_page")]
        public string PerPage { get; init; } = null!;

        [JsonProperty("count")]
        public string Count { get; init; } = null!;

        [JsonProperty("total_count")]
        public string TotalCount { get; init; } = null!;
    }
}
