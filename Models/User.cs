using System;

namespace AnimeWebProject.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public byte[] Avatar { get; set; }
        public byte[] BackgroundImage { get; set; }
    }
}