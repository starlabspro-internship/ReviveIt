namespace Domain.Entities
{
    public class UserCategory
    {
        public int UserCategoryId { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
