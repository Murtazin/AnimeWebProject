using System;

namespace AnimeWebProject.Models.ViewModels
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public long Date { get; set; }
        public string CountOfSeasons { get; set; }
        public string CountOfEpisodesPerSeason { get; set; }
        public string Director { get; set; }
        public byte[] ContentImage { get; set; }
        public string Status { get; set; }
        public string Rating { get; set; }
    }
}