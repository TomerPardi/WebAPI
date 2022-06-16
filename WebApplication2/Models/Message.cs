using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Message
    {
        public Message(string Content, string Sender, string Receiver)
        {
            this.Content = Content;
            this.Sender = Sender;
            this.Receiver = Receiver;
            Created = DateTime.Now.ToString("s");
        }
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        [DataType(DataType.DateTime)]
        public string Created { get; set; }

        // fg's
        public String UserId { get; set; }
        public String ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}
