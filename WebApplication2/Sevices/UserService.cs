using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public class UserService : IUserService
    {
        private readonly WebAPIContext _context;
        //public List<User> users = new();

        // FIREBASE +++++++++++++++++++++++++++++++++++++++++ FIREBASE
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

        // FIREBASE +++++++++++++++++++++++++++++++++++++++++ FIREBASE


        public UserService(WebAPIContext context)
        {
            _context = context;
            /*users.Add(new User("alice", "123"));
            users.Add(new User("bob", "456"));
            users.Add(new User("dan", "123"));
            users.Add(new User("tom", "123"));
            users.Add(new User("peter", "123"));*/
        }

        // USER +++++++++++++++++++++++++++++++++++++++++ USER

        public async Task CreateUser(string Id, string Password)
        {
            User user = new User(Id, Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<User> GetByIdAsync(string Id)
        {
            User user = await _context.User.FirstOrDefaultAsync(m => m.Id == Id);
            return user;
        }

        public Task UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        // USER +++++++++++++++++++++++++++++++++++++++++ USER

        // CONTACT +++++++++++++++++++++++++++++++++++++++++ CONTACT

        public async Task CreateContact(string Self, string UserId, string Name, string Server)
        {
            User user = await _context.User.FirstOrDefaultAsync(m => m.Id == Self);
            Contact contact = new Contact(UserId, Name, Server);
            contact.User = user;
            contact.UserId = Self;
            contact.Last = " ";
            // create and add new contact to self Contacts list via passed params
            user.Contacts.Add(contact);
            //_context.Contact.Add(contact);
            await _context.AddAsync(contact);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateContact(Contact contact, string Name, string Server)
        {

            contact.Name = Name;
            contact.Server = Server;
            _context.Update(contact);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteContactAsync(string self, string toRemove)
        {
            var contact = await _context.Contact.FirstOrDefaultAsync
                (m => m.Id == toRemove && m.UserId == self);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        public async Task<List<Contact>> GetAllContactsAsync(string Id)
        {
            var contacts = await _context.Contact.
                Where(contact => contact.UserId == Id).ToListAsync();

            // sort contacts by the last message
            if (contacts.Count == 0) { return contacts; }
            contacts.Sort(LastMessageComp);
            return contacts;
        }

        public async Task<Contact> GetContact(string userId, string owner)
        {
            var contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == userId && m.UserId == owner);
            return contact;
        }

        // CONTACT +++++++++++++++++++++++++++++++++++++++++ CONTACT

        // MESSAGE +++++++++++++++++++++++++++++++++++++++++ MESSAGE

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



        /*public List<Message> GetAllMessages(Contact contact)
        {
            var messages = _context.Message.Where(i => i.Contact.Id == contact.Id).ToList();
            return messages;

        }*/
        public async Task<List<Message>> GetAllMessagesAsync(string self, string id)
        {
            
            var messages = await _context.Message.Where(i => i.UserId == self && 
            i.ContactId == id).ToListAsync();
            return messages;
            
            /*catch (Exception)
            {
                return null;
            }*/
            
        }

        public async Task<Message> GetMessageByIdAsync(string selfID, string contactID, int messageID)
        {
            var message = await _context.Message.FirstOrDefaultAsync(m => m.Id == messageID);
            return message;
        }

        public async Task DeleteMessageByIdAsync(string selfID, string contactID, int messageID)
        {
            var message = await _context.Message.FindAsync(messageID);
            if (message != null)
            {
                _context.Remove(message);
            }

        }

        public async Task AddMessageAsync(string SelfID, string contactID, string message, bool isSelf)
        {

            var contact = await GetContact(contactID, SelfID);

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

            contact.lastdate = newMessage.Created;
            contact.Last = message;

            await UpdateContact(contact, contact.Name, contact.Server);
            await _context.AddAsync(newMessage
                );
            await _context.SaveChangesAsync();
        }

        public async Task ChangeMessageAsync(string selfID, string content, string contactID, int messageID)
        {
            Message message = await GetMessageByIdAsync(selfID, contactID, messageID);
            
            if (message != null)
            {
                message.Content = content;
                _context.Message.Update(message);
                await _context.SaveChangesAsync();
            }
        }

        // MESSAGE +++++++++++++++++++++++++++++++++++++++++ MESSAGE

    }
}
