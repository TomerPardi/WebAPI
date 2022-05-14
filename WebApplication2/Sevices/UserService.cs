using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public class UserService : IUserService
    {
        public List<User> users = new();

        public UserService()
        {
            // users.Add(new User("alice", "123"));
            // users.Add(new User("bob", "456"));
        }

        public void CreateUser(string Id, string Password)
        {
            users.Add(new User(Id, Password));
        }

        public User GetById(string Id)
        {
            return (users.Find(x => x.Id == Id));
        }

        public void UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAllContacts(string Id)
        {
            User user = GetById(Id);
            return user.Contacts;
        }
    }
}
