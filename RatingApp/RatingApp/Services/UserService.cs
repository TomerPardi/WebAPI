using RatingApp.Models;

namespace RatingApp.Services
{
    public class UserService : IUserService
    {
        public List<User> _users = new();
        public void DeleteUser(int id)
        {
            _users.Remove(GetUser(id));
        }

        public void EditUser(int id, int rating)
        {
            User user = GetUser(id);
            user.Rating = rating;
        }

        public void CreateUser(int rating, string name, string opinion)
        {
            int id = _users.Max(x => x.Id) + 1;
            DateTime localDate = DateTime.Now;
            string time = localDate.ToString();
            _users.Add(new User() { Id = id, Name = name, Opinion = opinion, Rating = rating, Time = time });
        }
        public void CreateUser(User user)
        {
            int id = 0;
            if (_users.Count() == 0)
            {
                id = 1;
            }
            else
            {
                id = _users.Max(x => x.Id) + 1;
            }

            DateTime localDate = DateTime.Now;
            string time = localDate.ToString();
            string name = user.Name;
            string opinion = user.Opinion;
            int rating = user.Rating;

            _users.Add(new User() { Id = id, Name = name, Opinion = opinion, Rating = rating, Time = time });
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUser(int id)
        {
            return _users.Find(x => x.Id == id);
        }

        public double GetAVG()
        {
            if (_users.Count() == 0)
            {
                return 0;
            }
            double avg = 0;
            int sum = 0;
            foreach (User user in GetAllUsers())
            {
                sum += user.Rating;

            }
            return sum / (double)_users.Count();
        }

    }
}
