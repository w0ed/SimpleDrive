using Microsoft.EntityFrameworkCore;
using SimpleDrive.Model;

namespace SimpleDrive.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<BlobEntity> Blobs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BlobEntity>().ToTable("Blobs");
        }
    }
}
