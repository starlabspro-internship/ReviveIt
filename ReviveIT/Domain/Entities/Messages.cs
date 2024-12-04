namespace Domain.Entities
{
    public class Messages
    {
        public int MessageID { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }

        public string SenderID { get; set; }
        public Users Sender { get; set; }
        public string RecipientID { get; set; }
        public Users Recipient { get; set; }

        public int ChatSessionId { get; set; } 
        public ChatSession ChatSession { get; set; } 

        public bool Viewed { get; set; } 
    }
}
