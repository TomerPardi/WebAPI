﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Sevices;
using WebApplication2.Models;


namespace WebAPI.Controllers
{
    public class CredentialsPayload
    {
        public string username { get; set; }
        public string password { get; set; }
    }

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
        public IActionResult getSelf()
        {
            string self = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(self))
            {
                return Unauthorized();
            }
            return Ok(self);
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


        private async 

        Task Signin(User user)
        {
            Console.WriteLine(user.Id+" "+user.Password);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
           
        }


        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CredentialsPayload data)
        {
            // {"username":"alice", "password":"123"}
            //validation logic
            User user = service.GetById(data.username);
            if (user == null) return BadRequest();
            else
            {
                if (user.Password != data.password) return BadRequest();
                else
                {

                    // TODO: create session for logged in user!
                    await Signin(user);
                    Console.WriteLine("My life is: "+HttpContext.User.FindFirstValue(ClaimTypes.Name));
                    return Ok(user);
                }
            }
        }


    }

}
