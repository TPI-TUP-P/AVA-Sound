using System.Text.Json;
using Domain.Entities;
using Domain.Objects.Statistics;
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


            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.Duration).IsRequired();
                entity.Property(e => e.AudioBig).IsRequired();
                entity.Property(e => e.DateUpload).IsRequired();
                entity.Property(e => e.Views).IsRequired();
                entity.Property(e => e.IdArtist).IsRequired();
                entity.HasOne(e => e.Artist)
                    .WithMany(u => u.Songs)
                    .HasForeignKey(e => e.IdArtist);

             
            });

            modelBuilder.Entity<ReproductionsList>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdUser).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.IsPublic).IsRequired();
                entity.Property(e => e.Creation).IsRequired();  
                entity.HasMany(e => e.Songs)
                    .WithMany()
                 ;   
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdUser).IsRequired();
                entity.Property(e => e.IdSong).IsRequired();
            });

            modelBuilder.Entity<Statistic>().Property(s=> s.Reproductions).HasConversion(
                v=> JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
        v => JsonSerializer.Deserialize<List<SongReproduction>>(v, (JsonSerializerOptions)null)      
              );


          modelBuilder.Entity<Statistic>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Statistic>(s => s.IdUser);

            // (entity =>
            // {
                
            //     entity.HasKey(e => e.Id);
            //     entity.Property(e => e.IdUser).IsRequired();

            // });

            modelBuilder.Entity<InfoUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdUser).IsRequired();
            });
            
            

           

            base.OnModelCreating(modelBuilder);
        }
        
  

    }
}