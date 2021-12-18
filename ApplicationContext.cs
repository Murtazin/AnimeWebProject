using AnimeWebProject.Models;
using AnimeWebProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebProject
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Content> Content { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> option)
            : base(option)
        {

        }
    }
}