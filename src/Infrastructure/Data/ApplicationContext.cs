using System.Text.Json;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
              .ValueGeneratedNever();
                entity.Property(e => e.Title).IsRequired();
                entity.HasMany(e => e.Songs)
                    .WithOne()
                    .HasForeignKey(s => s.IdAlbum);
            });

            

           

            base.OnModelCreating(modelBuilder);
        }
        
  

    }
}