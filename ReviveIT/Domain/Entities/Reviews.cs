namespace Domain.Entities
{
    public class Reviews
    {
        public int ReviewID { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }

        public string ReviewedUserId { get; set; }
        public Users ReviewedUser { get; set; }
    }
}