using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Contact
    {
        public Contact(string Id, string Name, string Server)
        {
            this.Id = Id;
            this.Name = Name;
            this.Server = Server;
            this.Last = null;
            this.lastdate = DateTime.Now.ToString("s");
            this.Messages = new();
        }
        // Id represents username
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        [DataType(DataType.DateTime)]
        public string lastdate { get; set; }
        public List<Message> Messages { get; set; }
    }
}