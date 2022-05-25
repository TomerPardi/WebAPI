using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IUserService service;

        public RegisterController(IUserService s)
        {
            service = s;
        }


        // POST api/<RegisterController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CredentialsPayload data)
        {
            if (service.GetById(data.username) != null) return StatusCode(StatusCodes.Status409Conflict);
            service.CreateUser(data.username, data.password);
            return Ok();
        }
    }
}
