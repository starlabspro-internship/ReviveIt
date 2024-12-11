namespace Domain.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public ICollection<OperatingCity> OperatingCities { get; set; }
    }
}
