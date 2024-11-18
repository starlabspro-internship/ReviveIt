namespace Domain.Entities
{
    public class UserRefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
    }
}
