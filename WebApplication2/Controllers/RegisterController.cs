using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;
using WebApplication2.Models;

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
            User user = await service.GetByIdAsync(data.username);
            if (user != null) return BadRequest();
            service.CreateUser(data.username, data.password);
            return Ok();
        }
    }
}
