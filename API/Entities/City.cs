
namespace API.Entities;
    public class City
    {
        public int Id { get; set; }  

        public string Name { get; set; }
         public Governorate Governorate { get; set; }
         
        public int GovernorateId { get; set; }    
    }
