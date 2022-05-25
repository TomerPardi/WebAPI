using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public IConfiguration _configuration;
        private readonly IUserService service;
        public LoginController(IUserService s, IConfiguration c)
        {
            service = s;
            _configuration = c;
        }


        [Route("/Logout")]
        [HttpGet]
        [Authorize]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

        [Authorize]
        [Route("/Self")]
        [HttpGet]
        public IActionResult getSelf()
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(selfID))
            {
                return Unauthorized();
            }
            return Ok(selfID);
        }

        [Route("/Server")]
        [HttpGet]
        public string getServer()
        {
            return HttpContext.Request.Host.ToString();
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

                    var token = CreateToken(data.username);
                    Response.Cookies.Append("token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    });
                    return Ok(user);
                }
            }
        }

        private string CreateToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtParams:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim("UserId", username)
            };

            var token = new JwtSecurityToken(
                _configuration["JwtParams:Issuer"],
                _configuration["JwtParams:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: mac
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
