using Microsoft.AspNetCore.Mvc.RazorPages;
using ReferralRock.Model;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


public class PageModelWithHttpClient : PageModel
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

    public async Task<MemberApiResponse> GetMembers(string memberId = null, int? offset = null, int? count = null)
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

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MemberApiResponse apiResponse = JsonSerializer.Deserialize<MemberApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return apiResponse;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<List<Referral>> GetReferrals(string memberId, string query = null)
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

        var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        var apiUrl = $"api/referrals?{queryString}";

        using (var httpClient = CreateClient())
        {
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ReferralApiResponse apiResponse = JsonSerializer.Deserialize<ReferralApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return apiResponse.Referrals;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<Referral> CreateReferral(NewReferral referral, string memberId)
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

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                NewReferralApiResponse apiResponse = JsonSerializer.Deserialize<NewReferralApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return apiResponse.Referral;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<bool> DeleteReferral(string referralId, string memberId)
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

            return response.IsSuccessStatusCode;
        }
    }

    public async Task<string> UpdateReferral(string referralId, Referral referral)
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

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<UpdateQueryResponse> apiResponse = JsonSerializer.Deserialize<List<UpdateQueryResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return apiResponse[0].resultInfo.Message;
            }
            else
            {
                return null;

            }
        }
    }
}