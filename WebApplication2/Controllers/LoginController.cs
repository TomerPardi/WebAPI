using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService service;


        public LoginController(IUserService s)
        {
            service = s;
        }


        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(string value)
        {
            if (service.GetById(value)
            {
                return Ok(); 

            }
            else return StatusCode(401, "not foo");
            //validation logic
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
