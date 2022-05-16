using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public void Post([FromBody] Payload data)
        {
            service.CreateUser(data.username, data.password);
        }
    }
}
