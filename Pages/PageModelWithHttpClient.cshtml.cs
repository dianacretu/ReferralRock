﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using ReferralRock.Model;
using ReferralRock.Pages;
using System.Text;
using System.Text.Json;


public class PageModelWithHttpClient : PageModel, IPageModelWithHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public PageModelWithHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    private HttpClient CreateClient()
    {
        string apiKey = _configuration["AppSettings:ApiKey"];
        var httpClient = _httpClientFactory.CreateClient("client");

        httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + apiKey);
        return httpClient;
    }

    public async Task<MemberApiResponse> GetMembers(string? memberId = null, int? offset = null, int? count = null)
    {
        var parameters = new Dictionary<string, string>
            {
                { "programId", _configuration["AppSettings:ProgramId"] }
            };

        if (!string.IsNullOrEmpty(memberId))
        {
            parameters.Add("query", memberId);
        }

        if (offset != null)
        {
            parameters.Add("offset", offset.ToString());
        }

        if (count != null)
        {
            parameters.Add("count", count.ToString());
        }

        var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        var apiUrl = $"api/members?{queryString}";

        using (var httpClient = CreateClient())
        {
            var response = await httpClient.GetAsync(apiUrl);
            var content = await response.Content.ReadAsStringAsync();
            MemberApiResponse apiResponse = JsonSerializer.Deserialize<MemberApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return apiResponse;
        }
    }

    public async Task<ReferralApiResponse> GetReferrals(string memberId, string? query = null, int? offset = null, int? count = null)
    {
        var parameters = new Dictionary<string, string>
        {
            { "programId", _configuration["AppSettings:ProgramId"] },
            { "memberId", memberId },
        };

        if (!string.IsNullOrEmpty(query))
        {
            parameters.Add("query", query);
        }

        if (offset != null)
        {
            parameters.Add("offset", offset.ToString());
        }

        if (count != null)
        {
            parameters.Add("count", count.ToString());
        }

        var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        var apiUrl = $"api/referrals?{queryString}";

        using (var httpClient = CreateClient())
        {
            var response = await httpClient.GetAsync(apiUrl);
            var content = await response.Content.ReadAsStringAsync();
            ReferralApiResponse apiResponse = JsonSerializer.Deserialize<ReferralApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return apiResponse;
        }
    }

    public async Task<NewReferralApiResponse> CreateReferral(NewReferral referral, string memberId)
    {
        var parameters = new Dictionary<string, string>
        {
            { "programId", _configuration["AppSettings:ProgramId"] },
            { "memberId", memberId },
        };

        var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        var apiUrl = $"api/referrals?{queryString}";

        using (var httpClient = CreateClient())
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(referral), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            NewReferralApiResponse apiResponse = JsonSerializer.Deserialize<NewReferralApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return apiResponse;
        }
    }

    public async Task<DeleteApiResponse> DeleteReferral(string referralId, string memberId)
    {
        var apiUrl = $"api/referral/remove";

        var queries = new List<DeleteQuery>();
        var info = new Info
        {
            primaryInfo = new PrimaryInfo
            {
                referralId = referralId,
            }
        };

        var queryApi = new DeleteQuery
        {
            query = info
        };

        queries.Add(queryApi);

        using (var httpClient = CreateClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(httpClient.BaseAddress + apiUrl),
                Content = new StringContent(JsonSerializer.Serialize(queries), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            List<DeleteApiResponse> apiResponse = JsonSerializer.Deserialize<List<DeleteApiResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return apiResponse[0];
        }
    }

    public async Task<UpdateQueryResponse> UpdateReferral(string referralId, ReceivedReferral referral)
    {
        var apiUrl = $"api/referral/update";

        var queries = new List<UpdateQuery>();
        var info = new Info
        {
            primaryInfo = new PrimaryInfo
            {
                referralId = referralId,
            }
        };

        var queryApi = new UpdateQuery
        {
            query = info,
            referral = referral
        };

        queries.Add(queryApi);

        using (var httpClient = CreateClient())
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(queries), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            List<UpdateQueryResponse> apiResponse = JsonSerializer.Deserialize<List<UpdateQueryResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return apiResponse[0];
        }
    }
}