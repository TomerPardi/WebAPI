using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;
using WebApplication2.Models;

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
        public IActionResult Post(string value, string password)
        {
            //validation logic
            User user = service.GetById(value);
            if (user == null) return BadRequest();
            else
            {
                if (user.Password != password) return BadRequest();
                else
                {
                    // TODO: create session for logged in user!
                    return Ok(user);
                }
            }
            
        }

       
    }
}
