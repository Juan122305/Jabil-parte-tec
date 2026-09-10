using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Movie> Movies => Set<Movie>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.ToTable("Director");
                entity.HasKey(d => d.PKDirector);
                entity.Property(d => d.PKDirector).HasColumnName("PKDirector").ValueGeneratedOnAdd();
                entity.Property(d => d.Name).HasColumnName("Name").HasMaxLength(100);
                entity.Property(d => d.Age).HasColumnName("Age");
                entity.Property(d => d.Active).HasColumnName("Active");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movies");
                entity.HasKey(m => m.PKMovies);
                entity.Property(m => m.PKMovies).HasColumnName("PKMovies").ValueGeneratedOnAdd();
                entity.Property(m => m.Name).HasColumnName("Name").HasMaxLength(100);
                entity.Property(m => m.Gender).HasColumnName("Gender").HasMaxLength(50);
                entity.Property(m => m.Duration).HasColumnName("Duration").HasColumnType("time");
                entity.Property(m => m.FKDirector).HasColumnName("FKDirector").IsRequired();

                entity.HasOne(m => m.Director)
                      .WithMany(d => d.Movies)
                      .HasForeignKey(m => m.FKDirector)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}