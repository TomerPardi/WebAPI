using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;
using WebAPI.Sevices;


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
        {
            try
            {
                await _hubContext.Clients.Group(data.to).SendAsync("Changed");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            service.CreateContact(data.to, data.from, data.from, data.server);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
