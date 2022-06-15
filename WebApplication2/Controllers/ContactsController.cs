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
            return service.GetAllContacts(selfID);
        }

        // GET api/<ContactsController>/{user}
        [HttpGet]
        [Route("{user}")]
        public IActionResult Get(string user)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var contact = service.GetAllContacts(selfID).Find(i => i.Id == user);
            if (contact == null) return NotFound();

            return Ok(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] ContactPayload data)
        {
            Console.WriteLine("Contacts post");
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            var user = service.GetById(selfID);
            var contact = user.Contacts.FindAll(x => x.Id == data.id);

            if (selfID == data.id) return StatusCode(StatusCodes.Status409Conflict);
            if (contact.Count != 0) return StatusCode(StatusCodes.Status409Conflict);
            if (service.GetById(data.id) == null) return NotFound();

            var sourceServer = HttpContext.Request.Host.ToString();
            service.CreateContact(selfID, data.id, data.name, data.server);
            // create a contact at the other side
            Console.Write("Created contact " + data.id + " @ " + selfID + " ");
            return StatusCode(StatusCodes.Status201Created);
            
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PutPayload data, string id)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            var user = service.GetAllContacts(selfID).Find(x => x.Id == id);

            if (user == null) return NotFound();
            service.UpdateContact(user, data.name, data.server);
            return StatusCode(StatusCodes.Status201Created);

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

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
        public async Task<IActionResult> PostMessageAsync([FromBody] MessagePayload data, string contactID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;
            service.AddMessage(selfID, contactID, data.content, true);
          /*  try
            {
                String fromToken = service.getTokenByUser(contactID);
                var message = new FirebaseAdmin.Messaging.Message()
                {
                    Notification = new Notification()
                    {
                        Body = "from " + selfID + ":" + data.content,
                        Title = "New Message",
                    },
                    Data = new Dictionary<string, string>()
                    {
                        { "sentFrom", contactID },
                        { "content", data.content },

                    },
                    Token = fromToken,
                };
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }*/
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("{contactID}/messages/{messageID}")]
        public void PutMessage([FromBody] MessagePayload data, string contactID, string messageID)
        {
            var selfID = HttpContext.User.FindFirst("UserId")?.Value;

            service.ChangeMessage(selfID, data.content, contactID, int.Parse(messageID));
        }


    }
}
