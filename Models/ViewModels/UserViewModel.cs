using System;
using Microsoft.AspNetCore.Http;

namespace AnimeWebProject.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Avatar { get; set; }
        public bool IsAuthorized = false;
    }
}