using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Sevices;
using WebApplication2.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("/Logout")]
        [HttpGet]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait(); // TODO: check if wait needed?
        }


        [Route("/Self")]
        [HttpGet]
        public string getSelf()
        {
            return (HttpContext.User.FindFirstValue(ClaimTypes.Name));
        }

        [Route("/Server")]
        [HttpGet]
        public string getServer()
        {
            return HttpContext.Request.Host.ToString();
        }

        private readonly IUserService service;


        public LoginController(IUserService s)
        {
            service = s;
        }


        private async void Signin(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // empty?
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
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
                    Signin(user);
                    return Ok(user);
                }
            }
        }


    }

}
