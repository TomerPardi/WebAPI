﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    public class TransferPayload
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }

    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IUserService service;
        public TransferController(IUserService s)
        {
            service = s;
        }

        // POST api/<TransferController>
        [HttpPost]
        public IActionResult Post([FromBody] TransferPayload data)
        //public IActionResult Post(string from, string to, string content)
        {
            service.AddMessage(data.To, data.From, data.Content, false);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}