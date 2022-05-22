using RatingApp.Models;

namespace RatingApp.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUser(int id);
        public void CreateUser(int rating, string name, string opinion);
        public void CreateUser(User user);
        public void DeleteUser(int id);
        public void EditUser(int id, int rating);

        public double GetAVG();

    }
}
