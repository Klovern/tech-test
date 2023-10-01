using Microsoft.AspNetCore.Mvc;

namespace TwitchService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TwitchController : ControllerBase
    {
        public TwitchController()
        {
                
        }

        public ActionResult Index()
        {
            return Ok("---> Alive");
        }
    }
}
