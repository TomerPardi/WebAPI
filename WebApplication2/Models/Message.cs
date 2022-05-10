namespace WebApplication2.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string BodyText { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Subject { get; set; }


        // need to add BodyImage, BodyAudio, BodyVideo etc.

    }
}
