using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;
using WebApplication2.Models;
using System.Text.Json;
using System.Security.Claims;

namespace WebAPI.Controllers
{

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
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            return service.GetAllContacts(selfID);// TODO: maybe return partial contacts list, without all the messages?
        }

        // GET api/<ContactsController>/{user}
        [HttpGet("{user}")]
        public IActionResult Get(string user)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var contact = service.GetAllContacts(selfID).Find(i => i.Id == user);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post(string UserId, string name, string server)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);


            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            service.CreateContact(selfID, UserId, name, server);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, string name, string server)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var user = service.GetAllContacts(selfID).Find(x => x.Id == id);

            if (user == null) return NotFound();
            service.UpdateContact(user, name, server);
            return StatusCode(StatusCodes.Status201Created);

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);


            //var Id = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            bool b = service.DeleteContact(selfID, id);
            if (!b) return NotFound();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /** MESSAGES **/

        // GET api/<ContactsController>/{user}/messages
        [HttpGet]
        [Route("{id}/messages")]
        public IActionResult GetAllMessages(string id)
        {

            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            return Ok(service.GetAllMessages(selfID, id));
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpGet]
        [Route("{contactID}/messages/{messageID}")]
        public IActionResult GetMessageById(string contactID, int messageID)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            return Ok(service.GetMessageById(selfID, contactID, messageID));
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpDelete]
        [Route("{contactID}/messages/{messageID}")]
        public IActionResult DeleteMessageById(string contactID, int messageID)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            service.DeleteMessageById(selfID, contactID, messageID);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        [Route("{contactID}/messages")]
        //TODO: reminder,return [FromBody] to message
        public IActionResult PostMessage(string message, string contactID)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            service.AddMessage(selfID, contactID, message, true);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("{contactID}/messages/{messageID}")]
        public void PutMessage([FromBody] string message, string contactID, int messageID)
        {
            string selfID = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            service.ChangeMessage(selfID, message, contactID, messageID);
        }
    }
}
