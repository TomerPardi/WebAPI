using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Contact
    {
        // Id represents username
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        [DataType(DataType.DateTime)]
        public string LastDate { get; set; }
        public List<Message> Messages { get; set; }
    }
}