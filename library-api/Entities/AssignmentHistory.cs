using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Entities;

public class AssignmentHistory
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime returneDate { get; set; }

    // protected void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<AssignmentHistory>()
    //         .HasOne(h => h.Student)
    //         .WithMany(s => s.AssignmentHistory) 
    //         .HasForeignKey(h => h.StudentId)
    //         .OnDelete(DeleteBehavior.SetNull);
    //
    //     modelBuilder.Entity<AssignmentHistory>()
    //         .HasOne(h => h.Book)
    //         .WithMany(b => b.AssignmentHistories) 
    //         .HasForeignKey(h => h.BookId)
    //         .OnDelete(DeleteBehavior.SetNull);
    // }//Bir öğrenci veya kitap silindiğinde, geçmiş kayıtlarının silinmemesi için ilgili alanların boş (null) bırakılmasını sağlar.
}