namespace API.Entities;
    public class Governorate 
    {
        public int Id { get; set; }  

        public string Name { get; set; }
        public int CountryId { get; set; }
         public Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }  

    }
