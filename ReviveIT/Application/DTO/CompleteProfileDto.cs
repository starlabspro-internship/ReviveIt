namespace Application.DTO
{
    public class CompleteProfileDto
    {
        public string Phone { get; set; }
        public string Description { get; set; }
        public List<int> Cities { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
        public int? Experience { get; set; }
    }
}