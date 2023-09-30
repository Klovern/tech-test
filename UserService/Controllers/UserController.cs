using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("Health")]
        public ActionResult Health()
        {
            return Ok("--> Healthy");
        }
    }
}