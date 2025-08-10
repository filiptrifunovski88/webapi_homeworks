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

        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }
                if (index >= StaticDb.Usernames.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }
                return Ok(StaticDb.Usernames[index]);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
