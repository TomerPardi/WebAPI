using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {

        public User(string Id, string Password)
        {
            this.Id = Id;
            this.Password = Password;
            Contacts = new();
        }

        // Id represents username
        public string Id { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Contact> Contacts { get; set; }

    }
}
