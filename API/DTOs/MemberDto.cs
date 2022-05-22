using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string FacebookUrl { get; set; }
        public string WorkExperience { get; set; }
        public string address { get; set; }
        public Craft Craft { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public bool IsLiked { get; set; }
    }
}