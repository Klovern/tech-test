using static TwitchService.SyncDataServices.TwitchClient;

namespace TwitchService.SyncDataServices
{
    public interface ITwitchClient
    {
        public Task<TwitchUserRepresentation?> GetTwitchRepresentation(string userName);
    }
}
