using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Sevices;
using WebApplication2.Models;

namespace WebAPI.Controllers
{


    public class MessagePayload
    {
        public string content { get; set; }

    }

    public class ContactPayload
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
    }

    public class PutPayload
    {
        public string name { get; set; }
        public string server { get; set; }
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

        /** ####################################################################### **/
        /** CONTACTS **/


        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            // who made the get request
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var contacts = await service.GetAllContactsAsync(selfID);
            return contacts;
        }

        // GET api/<ContactsController>/{user}
        [HttpGet]
        [Route("{user}")]
        public async Task<IActionResult> GetAsync(string user)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var contacts = await service.GetAllContactsAsync(selfID);
            var contact = contacts.Find(i => i.Id == user);
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ContactPayload data)
        {
            Console.WriteLine("Contacts post");
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var contacts = await service.GetAllContactsAsync(selfID);

            var contact = contacts.FindAll(x => x.Id == data.id);

            if (selfID == data.id) return StatusCode(StatusCodes.Status409Conflict);
            if (contact.Count != 0) return StatusCode(StatusCodes.Status409Conflict);
            if (await service.GetByIdAsync(data.id) == null) return NotFound();

            var sourceServer = HttpContext.Request.Host.ToString();
            await service.CreateContact(selfID, data.id, data.name, data.server);
            // create a contact at the other side
            Console.Write("Created contact " + data.id + " @ " + selfID + " ");
            return StatusCode(StatusCodes.Status201Created);
            
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] PutPayload data, string id)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var contacts = await service.GetAllContactsAsync(selfID);

            var user = contacts.Find(x => x.Id == id);

            if (user == null) return NotFound();
            await service.UpdateContact(user, data.name, data.server);
            return StatusCode(StatusCodes.Status201Created);

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            bool b = await service.DeleteContactAsync(selfID, id);
            if (!b) return NotFound();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        /** ####################################################################### **/
        /** MESSAGES **/

        // GET api/<ContactsController>/{user}/messages
        [HttpGet]
        [Route("{id}/messages")]
        public async Task<IActionResult> GetAllMessagesAsync(string id)
        {

            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var messages = await service.GetAllMessagesAsync(selfID, id);

            return Ok(messages);
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpGet]
        [Route("{contactID}/messages/{messageID}")]
        public async Task<IActionResult> GetMessageByIdAsync(string contactID, int messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var message = await service.GetMessageByIdAsync(selfID, contactID, messageID);

            return Ok(message);
        }

        // GET api/<ContactsController>/{contactID}/messages/{messageID}
        [HttpDelete]
        [Route("{contactID}/messages/{messageID}")]
        public async Task<IActionResult> DeleteMessageByIdAsync(string contactID, int messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            await service.DeleteMessageByIdAsync(selfID, contactID, messageID);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost]
        [Route("{contactID}/messages")]
        public async Task<IActionResult> PostMessageAsync([FromBody] MessagePayload data, string contactID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            await service.AddMessageAsync(selfID, contactID, data.content, true);
          
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("{contactID}/messages/{messageID}")]
        public async Task PutMessageAsync([FromBody] MessagePayload data, string contactID, string messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            await service.ChangeMessageAsync(selfID, data.content, contactID, int.Parse(messageID));
        }


    }
}
