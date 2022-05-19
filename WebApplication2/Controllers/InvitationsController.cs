using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        public class InvitePayload
        {
            public string from { get; set; }
            public string to { get; set; }
            public string server { get; set; }
        }

        private readonly IUserService service;
        private readonly IHubContext<MessagesHub> _hubContext;
        public InvitationsController(IUserService s, IHubContext<MessagesHub> hubContext)
        {
            service = s;
            _hubContext = hubContext;
        }

        // body of post is {fron, to, server}
        // POST api/<InvitationsController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] InvitePayload data)
        //public IActionResult Post(string from, string to, string server)
        {
            try
            {
                await _hubContext.Clients.Group(data.to).SendAsync("Changed");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            // TODO: understand if we need to check if "to" user is exist
            // TODO: what nickname do we need to input?
            // I suppose that it needs to be "from"
            service.CreateContact(data.to, data.from, data.from, data.server);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
