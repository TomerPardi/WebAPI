using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public interface IUserService
    {
        public void CreateUser(string Id, string Password);

        public User GetById(string Id);
        public void UpdateUser(User User);
        public List<Contact> GetAllContacts(string Id);
        public void CreateContact(string Self, string UserId, string Name, string Server);
        public void UpdateContact(string Self, string UserId, string Name, string Server);
        public void UpdateContact(Contact contact, string Name, string Server);
        public bool DeleteContact(string self, string toRemove);
        public List<Message> GetAllMessages(string self, string id);
        public Message GetMessageById(string selfID, string contactID, int messageID);
        public void DeleteMessageById(string selfID, string contactID, int messageID);
        public void AddMessage(string SelfID, string contactID, string message, bool isSelf);
        public void ChangeMessage(string selfID, string message, string contactID, int messageID);

        public void insetTokenPair(String user, String token);

        public string getTokenByUser(String user);

        public void removeUser(String user);
    }
}
