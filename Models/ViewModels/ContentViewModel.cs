using System;
using Microsoft.AspNetCore.Http;

namespace AnimeWebProject.Models.ViewModels
{
    public class ContentViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Genre { get; set; }
        public string CountOfSeasons { get; set; }
        public string CountOfEpisodesPerSeason { get; set; }
        public string Director { get; set; }
        public IFormFile ContentImage { get; set; }
        public string Status { get; set; }
        public string Rating { get; set; }
    }
}