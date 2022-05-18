using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;
using WebApplication2.Models;
using System.Text.Json;
using System.Security.Claims;

namespace WebAPI.Controllers
{

    
    public class MessagePayload
    {
        public string Message { get; set; }
        public string ContactID { get; set; }

        public int MessageID { get; set; } = -1;


    }

    public class ContactPayload
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
    }
    [Route("api/contacts")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IUserService service;
        private readonly static string selfID;
        public ContactsController(IUserService s)
        {
            service = s;
        }


        /** CONTACTS **/


        // GET: api/<ContactsController>
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            // TODO: Here we need to get somehow the id of the connected user
            // who made the get request
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            return service.GetAllContacts(selfID);// TODO: maybe return partial contacts list, without all the messages?
        }

        // GET api/<ContactsController>/{user}
        [HttpGet]
        [Route("{user}")]
        public IActionResult Get(string user)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var contact = service.GetAllContacts(selfID).Find(i => i.Id == user);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        //public IActionResult Post(string UserId, string name, string server)
        public IActionResult Post([FromBody]ContactPayload data)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;


            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var sourceServer = HttpContext.Request.Host.ToString();
            service.CreateContact(selfID, data.UserId, data.Name, data.Server);
            // create a contact at the other side
            /*if(sourceServer == server)
            {
                service.CreateContact( UserId, selfID , selfID, server);
            }*/
            /*else
            {
                HttpClient httpClient = new HttpClient();
                var toSend = new { from = selfID, to = UserId, server = sourceServer };
                httpClient.PostAsJsonAsync(server + "/api/invitations/", toSend);

            }*/
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        //public IActionResult Put(string id, string name, string server)
        public IActionResult Put([FromBody] ContactPayload data)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var user = service.GetAllContacts(selfID).Find(x => x.Id == data.UserId);

            if (user == null) return NotFound();
            service.UpdateContact(user, data.Name, data.Server);
            return StatusCode(StatusCodes.Status201Created);

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;


            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            bool b = service.DeleteContact(selfID, id);
            if (!b) return NotFound();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        /** ####################################################################### **/
                                     /** MESSAGES **/

        // GET api/<ContactsController>/{user}/messages
        [HttpGet]
        [Route("{id}/messages")]
        public IActionResult GetAllMessages(string id)
        {

            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            return Ok(service.GetAllMessages(selfID, id));
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpGet]
        [Route("{contactID}/messages/{messageID}")]
        public IActionResult GetMessageById(string contactID, int messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            return Ok(service.GetMessageById(selfID, contactID, messageID));
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpDelete]
        [Route("{contactID}/messages/{messageID}")]
        public IActionResult DeleteMessageById(string contactID, int messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            service.DeleteMessageById(selfID, contactID, messageID);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        [Route("{contactID}/messages")]
        public IActionResult PostMessage([FromBody] MessagePayload data)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            service.AddMessage(selfID, data.ContactID, data.Message, true);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("{contactID}/messages/{messageID}")]
        //public void PutMessage([FromBody] string message, string contactID, int messageID)
        public void PutMessage([FromBody] MessagePayload data)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            service.ChangeMessage(selfID, data.Message, data.ContactID, data.MessageID);
        }
    }
}
