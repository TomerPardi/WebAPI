using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    public class TransferPayload
    {
        public string from { get; set; }
        public string to { get; set; }
        public string content { get; set; }
    }

    

    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IHubContext<MessagesHub> _hubContext;
        

        public TransferController(IUserService s, IHubContext<MessagesHub> hubContext)
        {
            service = s;
            _hubContext = hubContext;
        }

        // POST api/<TransferController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferPayload data)
        //public IActionResult Post(string from, string to, string content)
        {
            service.AddMessage(data.to, data.from, data.content, false);
            try
            {
               await _hubContext.Clients.Group(data.to).SendAsync("Changed");
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
