using Microsoft.AspNetCore.Mvc;
using TwitchService.SyncDataServices;
using static TwitchService.SyncDataServices.TwitchClient;

namespace TwitchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitchController : ControllerBase
    {
        private readonly ITwitchClient _client;
        public TwitchController(ITwitchClient twitchClient)
        {
                _client = twitchClient;
        }

        [Route("health")]
        public ActionResult Health()
        {
            return Ok("---> Alive");
        }

        [Route("profile/{userName}")]
        [HttpGet]
        public async Task<ActionResult<TwitchUserRepresentation>> LinkTwitchUser(string userName)
        {
            Console.WriteLine("--> Entered LinkTwitchUser");
            var user = await _client.GetTwitchRepresentation(userName);
            return Ok(user);
        }
    }
}
