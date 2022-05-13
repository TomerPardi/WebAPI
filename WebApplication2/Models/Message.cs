using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        [DataType(DataType.DateTime)]
        public string Created { get; set; }

        // need to add BodyImage, BodyAudio, BodyVideo etc.

    }
}
