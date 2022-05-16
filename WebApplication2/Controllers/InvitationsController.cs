using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IUserService service;
        public InvitationsController(IUserService s)
        {
            service = s;
        }

        // body of post is {fron, to, server}
        // POST api/<InvitationsController>
        [HttpPost]
        public IActionResult Post(string from, string to, string server)
        {
            // TODO: understand if we need to check if "to" user is exist
            // TODO: what nickname do we need to input?
            // I suppose that it needs to be "from"
            service.CreateContact(to, from, from, server);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
