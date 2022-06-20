using FirebaseAdmin.Messaging;
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
                String fromToken = service.getTokenByUser(data.to);
                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        
                        Title = "Invited",
                    },
                    Token = fromToken,
                };
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            try
            {
                await _hubContext.Clients.Group(data.to).SendAsync("Changed");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            await service.CreateContact(data.to, data.from, data.from, data.server);
            Console.Write("Invited contact "+data.to+" @ "+data.from+" ");
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
