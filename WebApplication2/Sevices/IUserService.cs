using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public interface IUserService
    {
        public void CreateUser(string Id, string Password);

        public User GetByIdAsync(string Id);
        public void UpdateUser(User User);
        public List<Contact> GetAllContactsAsync(string Id);
        public void CreateContact(string Self, string UserId, string Name, string Server);
        public void UpdateContact(Contact contact, string Name, string Server);
        public bool DeleteContactAsync(string self, string toRemove);
        public List<Message> GetAllMessagesAsync(string self, string id);
        public Message GetMessageByIdAsync(string selfID, string contactID, int messageID);
        public void DeleteMessageByIdAsync(string selfID, string contactID, int messageID);
        public void AddMessageAsync(string SelfID, string contactID, string message, bool isSelf);
        public void ChangeMessageAsync(string selfID, string message, string contactID, int messageID);

        public void insetTokenPair(String user, String token);

        public string getTokenByUser(String user);

        public void removeUser(String user);
    }
}
