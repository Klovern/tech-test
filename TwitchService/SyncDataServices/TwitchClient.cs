using System.Net;
using System.Text.Json;

namespace TwitchService.SyncDataServices
{
    public class TwitchClient : ITwitchClient
    {
        private readonly HttpClient _httpClient;

        public TwitchClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TwitchUserRepresentation?> GetTwitchRepresentation(string userName)
        {
            Console.WriteLine($"--> BaseAddress: {_httpClient?.BaseAddress?.ToString() ?? "Empty"}");

            try
            {
                var result = await _httpClient.GetAsync($"users?login={userName}");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var twitchUser = await result.Content.ReadAsStringAsync();
                    return result != null ? JsonSerializer.Deserialize<RootTwitchUser>(twitchUser)?.data?.FirstOrDefault() : default;
                }
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
      
            return default;
        }

        public record TwitchUserRepresentation 
        {
            public string id { get; set; }
            public string login { get; set; }
            public string display_name { get; set; }
            public string type { get; set; }
            public string broadcaster_type { get; set; }
            public string description { get; set; }
            public string profile_image_url { get; set; }
            public string offline_image_url { get; set; }
            public int view_count { get; set; }
            public DateTime created_at { get; set; }
        }

        public record RootTwitchUser
        {
            public List<TwitchUserRepresentation> data { get; set; }
        }
    }
}
