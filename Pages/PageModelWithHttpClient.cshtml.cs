using Microsoft.AspNetCore.Mvc.RazorPages;
using ReferralRock.Model;
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

        public async Task<List<Member>> GetMembers()
        {
            var parameters = new Dictionary<string, string>
            {
                { "programId", _configuration["AppSettings:ProgramId"] }
            };

            var queryString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            var apiUrl = $"api/members?{queryString}";

            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MemberApiResponse<Member> apiResponse = JsonSerializer.Deserialize<MemberApiResponse<Member>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return apiResponse.Members;
                }
                else
                {
                    return null;
                }
            }
        }
    }

