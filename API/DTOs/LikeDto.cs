namespace API.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl { get; set; }
       public string address { get; set; }
        public Craft Craft { get; set; }
        public bool IsLiked { get; set; }


    }
}