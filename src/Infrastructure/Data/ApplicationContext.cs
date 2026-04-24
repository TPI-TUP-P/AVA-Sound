using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Album> Albums { get; set;}
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InfoUser> InfoUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<ReproductionsList> ReproductionsLists { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options ) : base(options)
        {
            
        }

    }
}