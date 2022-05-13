using WebApplication2.Models;

namespace WebAPI.Sevices
{
    public interface IUserService
    {
        public void CreateUser(string Id, string Password);

        /// <param name="Id">Id is username</param>
        /// <returns>User by Id / NULL if not exist</returns>
        public User GetById(string Id);
        public void UpdateUser(User User);


    }
}
