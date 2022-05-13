using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {

        public User(string v, string pas)
        {
            Id = v;
            Password = pas;
            Contacts = new();

       
        }

        // Id represents username
        public string Id { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Contact> Contacts { get; set; }


        // propety of image?

        // where to place messages?
    }
}
