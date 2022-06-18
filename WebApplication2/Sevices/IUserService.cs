using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public interface IUserService
    {
        public Task CreateUser(string Id, string Password);
        public Task<User> GetByIdAsync(string Id);
        public Task UpdateUser(User User);
        public Task<List<Contact>> GetAllContactsAsync(string Id);
        public Task CreateContact(string Self, string UserId, string Name, string Server);
        public Task UpdateContact(Contact contact, string Name, string Server);
        public Task<bool> DeleteContactAsync(string self, string toRemove);
        public Task<List<Message>> GetAllMessagesAsync(string self, string id);
        public Task<Message> GetMessageByIdAsync(string selfID, string contactID, int messageID);
        public Task DeleteMessageByIdAsync(string selfID, string contactID, int messageID);
        public Task AddMessageAsync(string SelfID, string contactID, string message, bool isSelf);
        public Task ChangeMessageAsync(string selfID, string content, string contactID, int messageID);

        public void insetTokenPair(String user, String token);

        public string getTokenByUser(String user);

        public void removeUser(String user);
    }
}
