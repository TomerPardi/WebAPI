﻿using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;
using WebAPI.Sevices;

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
        {
            service.AddMessage(data.to, data.from, data.content, false);
            String fromToken = service.getTokenByUser(data.to);
            try
            {
                try
                {
                    var message = new Message()
                    {
                        Notification = new Notification()
                        {
                            Body = "from " + data.to + ":" + data.content,
                            Title = "New Message",
                        },
                        Data = new Dictionary<string, string>()
                    {
                        { "sentFrom", data.to },
                        { "content", data.content },

                    },
                        Token = fromToken,
                    };
                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                await _hubContext.Clients.Group(data.to).SendAsync("Changed");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
