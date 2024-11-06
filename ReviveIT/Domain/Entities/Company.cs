using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Company
    {
    
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }


    }
}
