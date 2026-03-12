using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AssignmentHistory> AssignmentHistories { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssignmentHistory>()
                .HasOne(ah => ah.Book)
                .WithMany(b => b.AssignmentHistories)
                .HasForeignKey(ah => ah.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentHistory>()
                .HasOne(ah => ah.Student)
                .WithMany(s => s.AssignmentHistories)
                .HasForeignKey(ah => ah.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}