namespace Domain.Entities
{
    public class OperatingCity 
    {
        public int OperatingCityId {  get; set; }
        public int CityId {  get; set; }
        public string userId { get; set; }

        public City City { get; set; }
        public Users User {  get; set; }
    }
}
