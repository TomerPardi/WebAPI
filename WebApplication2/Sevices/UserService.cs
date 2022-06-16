using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public class UserService : IUserService
    {
        private readonly WebAPIContext _context;
        public List<User> users = new();
        public Dictionary<String, String> userWithToken = new();

        public void insetTokenPair(String user,String token)
        {
            userWithToken[user] = token;
        }

        public string getTokenByUser(String user)
        {
            return userWithToken[user];
        }

        public void removeUser(String user)
        {
            userWithToken.Remove(user);
        }

        public UserService()
        {
            users.Add(new User("alice", "123"));
            users.Add(new User("bob", "456"));
            users.Add(new User("dan", "123"));
            users.Add(new User("tom", "123"));
            users.Add(new User("peter", "123"));
        }

        public void CreateUser(string Id, string Password)
        {
            users.Add(new User(Id, Password));
        }

        public async void CreateContact(string Self, string UserId, string Name, string Server)
        {
            User user = await _context.User.FirstOrDefaultAsync(m => m.Id == Self);
            Contact contact = new Contact(UserId, Name, Server);
            contact.User = user;
            contact.UserId = Self;
            // create and add new contact to self Contacts list via passed params
            user.Contacts.Add(contact);
            _context.Add(contact);
            await _context.SaveChangesAsync();
        }


        public async void UpdateContact(Contact contact, string Name, string Server)
        {

            contact.Name = Name;
            contact.Server = Server;
            _context.Update(contact);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteContactAsync(string self, string toRemove)
        {
            var contact = await _context.Contact.FindAsync(toRemove);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }


        public async Task<User> GetByIdAsync(string Id)
        {
            User user = await _context.User.FirstOrDefaultAsync(m => m.Id == Id);
            return user;
        }

        public void UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        private static int LastMessageComp(Contact x, Contact y)
        {
            if (x == null || x.lastdate == null)
            {
                if (y == null || y.lastdate == null)
                {
                    // If x is null and y is null, they're
                    // equal.
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater.
                    return 1;
                }
            }
            // if x is not null
            else
            {
                if (y == null || y.lastdate == null)
                {
                    return -1;
                }
                // if x isn't null and y isnt null
                else
                {
                    var parsedX = DateTime.Parse(x.lastdate);
                    var parsedY = DateTime.Parse(y.lastdate);
                    return -DateTime.Compare(parsedX, parsedY);
                }
            }
        }

        public async Task<List<Contact>> GetAllContactsAsync(string Id)
        {
            var contacts = await _context.Contact.Include(m => m.Messages).
                Where(i => i.User.Id == Id).ToListAsync();
            
            // sort contacts by the last message
            if (contacts.Count == 0) { return contacts; }
            contacts.Sort(LastMessageComp);
            return contacts;
        }


        public List<Message> GetAllMessages(Contact contact)
        {
            var messages = _context.Message.Where(i => i.Contact.Id == contact.Id).ToList();
            return messages;

        }
        public async Task<List<Message>> GetAllMessagesAsync(string self, string id)
        {
            try
            {
                var messages = await _context.Message.Where(i => i.Contact.Id == id).ToListAsync();
                return messages;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<Message> GetMessageByIdAsync(string selfID, string contactID, int messageID)
        {
            var message = await _context.Message.FindAsync(messageID);
            return message;
        }

        public async Task DeleteMessageByIdAsync(string selfID, string contactID, int messageID)
        {
            var message = await _context.Message.FindAsync(messageID);

        }

        public async Task AddMessageAsync(string SelfID, string contactID, string message, bool isSelf)
        {

            var contacts = await GetAllContactsAsync(SelfID);
            var contact = contacts.Result.

            string sender, receiver;
            if (isSelf)
            {
                sender = SelfID;
                receiver = contactID;
            }
            else
            {
                sender = contactID;
                receiver = SelfID;
            }
            Message newMessage = new(message, sender, receiver);
            newMessage.ContactId = contactID;
            newMessage.UserId = SelfID;

            mList.Add(newMessage);
            var user = users.Find(x => x.Id == SelfID);
            var contact = user.Contacts.Find(x => x.Id == contactID);
            contact.Last = message;
            contact.lastdate = newMessage.Created;
        }

        public async Task ChangeMessageAsync(string selfID, string content, string contactID, int messageID)
        {
            Message message = await _context.Message.FindAsync(messageID);
            
            if (message != null)
            {
                message.Content = content;
                _context.Message.Update(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
