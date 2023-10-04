using System.Net;
using System.Reflection.Metadata.Ecma335;
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
            Console.WriteLine($"--> BaseAddress: {_httpClient.BaseAddress.ToString() ?? "Empty"}");
            var xddd = await _httpClient.GetAsync($"users?login={userName}");

            if (xddd.StatusCode == HttpStatusCode.OK)
            {
                var result = await xddd.Content.ReadAsStringAsync();            
                return result != null ? JsonSerializer.Deserialize<RootTwitchUser>(result)?.data?.FirstOrDefault() : default;
            }
            return default;
        }

        public class TwitchUserRepresentation 
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

        public class RootTwitchUser
        {
            public List<TwitchUserRepresentation> data { get; set; }
        }
    }
}
