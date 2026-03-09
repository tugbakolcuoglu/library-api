using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AssignmentHistory> AssignmentHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // STUDENT - BOOK (1-1)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Student)
                .WithOne(s => s.Book)
                .HasForeignKey<Book>(b => b.StudentId)
                .OnDelete(DeleteBehavior.SetNull);

            // StudentId unique olmalı (1-1 için)
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.StudentId)
                .IsUnique();
      
            // Kitap - Geçmiş Kayıtları İlişkisi (1:N)
            // Bir kitabın birçok geçmiş kaydı (AssignmentHistory) olabilir
            modelBuilder.Entity<AssignmentHistory>()
                .HasOne(ah => ah.Book)
                .WithMany(b => b.AssignmentHistories) // Book içindeki ICollection ismiyle aynı olmalı
                .HasForeignKey(ah => ah.BookId)
                .OnDelete(DeleteBehavior.Restrict); // Kitap silindiğinde ilgili geçmiş kayıtları da silinsin

            //Öğrenci - Geçmiş Kayıtları İlişkisi (1:N)
            modelBuilder.Entity<AssignmentHistory>()
                .HasOne(ah => ah.Student)
                .WithMany(s => s.AssignmentHistory) // Student içindeki ICollection ismiyle aynı olmalı
                .HasForeignKey(ah => ah.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}