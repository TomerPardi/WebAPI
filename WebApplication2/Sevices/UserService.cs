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

        public void UpdateContact(Contact contact,string Name,string Server)
        {
  
            contact.Name = Name;
            contact.Server = Server;

        }

        public bool DeleteContact(string self,string toRemove)
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

        private static int latsMessageComp(Contact x, Contact y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal.
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater.
                    return -1;
                }
            }
            // if x is not null
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {

                    var parsedX = DateTime.Parse(x.LastDate);
                    var parsedY = DateTime.Parse(y.LastDate);
                    return DateTime.Compare(parsedX, parsedY);
                }
            }
        }

        public List<Contact> GetAllContacts(string Id)
        {
            User user = GetById(Id);
            // sort contacts by the last message
            if (user.Contacts.Count == 0) { return user.Contacts; }
            user.Contacts.Sort(latsMessageComp);
            return user.Contacts;
        }

        public List<Message> GetAllMessages(string self, string id)
        {
            return GetById(self).Contacts.Find(x => x.Id == id).Messages;
        }

        public Message GetMessageById(string selfID, string contactID, int messageID)
        {
            return GetAllMessages(selfID, contactID).FirstOrDefault(x => x.Id == messageID);
        }

        public void DeleteMessageById(string selfID, string contactID, int messageID)
        {
            GetAllMessages(selfID,contactID).RemoveAll(x => x.Id == messageID);
        }

        public void AddMessage(string SelfID, string contactID, string message, bool isSelf)
        {
            // TODO: update lastMessage's content and date.
            List<Message> mList = GetAllMessages(SelfID, contactID);
            int id;
            if (mList.Count == 0) id = 0;
            else id = mList.Max(x => x.Id) + 1;

            string sender,receiver;
            if (isSelf) {
                sender = SelfID;
                receiver = contactID;
            }
            else {
                sender = contactID;
                receiver = SelfID;
            }
            Message newMessage = new Message(id, message, sender, receiver);
            mList.Add(newMessage);

            if (isSelf)
            {
                
                HttpClient httpClient = new HttpClient();
                string server = GetAllContacts(SelfID).Find(x => x.Id == contactID).Server;
                var toSend = new { from = sender, to = receiver, content = message };
                httpClient.PostAsJsonAsync(server + "/api/transfer/", toSend);
            }
        }

        public void ChangeMessage(string selfID, string message, string contactID, int messageID)
        {
            var m = GetMessageById(selfID, contactID, messageID);
            if (m != null)
            {
                m.Content = message;

            }
        }
    }
}
