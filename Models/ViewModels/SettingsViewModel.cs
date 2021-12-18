using Microsoft.AspNetCore.Http;

namespace AnimeWebProject.Models.ViewModels
{
    public class SettingsViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public IFormFile Avatar { get; set; }
        public IFormFile BackgroundImage { get; set; }
        public string Username { get; set; }
    }
}