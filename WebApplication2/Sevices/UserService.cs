using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public class UserService : IUserService
    {
        public List<User> users = new();

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

        public void CreateContact(string Self, string UserId, string Name, string Server)
        {
            //find self in user list
            var user = users.Find(x => x.Id == Self);
            // create and add new contact to self Contacts list via passed params
            user.Contacts.Add(new Contact(UserId, Name, Server));
        }

        public void UpdateContact(string Self, string UserId, string Name, string Server)
        {
            //find self in user list
            var user = users.Find(x => x.Id == Self);

            var contact = user.Contacts.Find(x => x.Id == UserId);
            contact.Name = Name;
            contact.Server = Server;

        }

        public void UpdateContact(Contact contact, string Name, string Server)
        {

            contact.Name = Name;
            contact.Server = Server;

        }

        public bool DeleteContact(string self, string toRemove)
        {
            var user = users.Find(x => x.Id == self);
            return user.Contacts.RemoveAll(x => x.Id == toRemove) > 0;
        }


        public User GetById(string Id)
        {
            return (users.Find(x => x.Id == Id));
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

        public List<Contact> GetAllContacts(string Id)
        {
            User user = GetById(Id);
            // sort contacts by the last message
            if (user.Contacts.Count == 0) { return user.Contacts; }
            user.Contacts.Sort(LastMessageComp);
            return user.Contacts;
        }

        public List<Message> GetAllMessages(string self, string id)
        {
            var user = GetById(self).Contacts.Find(x => x.Id == id);
            if (user != null) return GetById(self).Contacts.Find(x => x.Id == id).Messages;
            else return null;
        }

        public Message GetMessageById(string selfID, string contactID, int messageID)
        {
            return GetAllMessages(selfID, contactID).FirstOrDefault(x => x.Id == messageID);
        }

        public void DeleteMessageById(string selfID, string contactID, int messageID)
        {
            var user = GetById(selfID).Contacts.Find(x => x.Id == contactID);
            // if last message, update in contact's Last and LastDate
            if (messageID == user.Messages.Count - 1 && messageID != 0)
            {
                Message m = GetMessageById(selfID, contactID, messageID - 1);
                user.Last = m.Content;
                user.lastdate = m.Created;
            }

            GetAllMessages(selfID, contactID).RemoveAll(x => x.Id == messageID);

        }

        public void AddMessage(string SelfID, string contactID, string message, bool isSelf)
        {
            List<Message> mList = GetAllMessages(SelfID, contactID);
            int id;
            if (mList.Count == 0) id = 0;
            else id = mList.Max(x => x.Id) + 1;

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
            Message newMessage = new(id, message, sender, receiver);
            mList.Add(newMessage);
            var user = users.Find(x => x.Id == SelfID);
            var contact = user.Contacts.Find(x => x.Id == contactID);
            contact.Last = message;
            contact.lastdate = newMessage.Created;
        }

        public void ChangeMessage(string selfID, string message, string contactID, int messageID)
        {
            var m = GetMessageById(selfID, contactID, messageID);
            if (m != null)
            {
                m.Content = message;
                var user = GetById(selfID).Contacts.Find(x => x.Id == contactID);
                // if last message, update in contact's Last and LastDate
                if (messageID == user.Messages.Count - 1 && messageID != 0)
                {
                    user.Last = message;
                    user.lastdate = DateTime.Now.ToString("s");
                }
            }
        }
    }
}
