using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace homework01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(StaticDb.Usernames);
        }

        [HttpGet("oneuser")]
        public IActionResult GetOneUser()
        {
            return Ok(StaticDb.Usernames[1]);
        }
    }
}
