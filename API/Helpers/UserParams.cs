namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }
        public int CountryId { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int CraftId { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}