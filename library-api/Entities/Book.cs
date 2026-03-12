using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities;

public class Book
{
    public Guid Id { get; set; }

    [MaxLength(100)] public string Title { get; set; } = null!;

    [MaxLength(100)] public string Author { get; set; } = null!;

    [NotMapped]
    public bool IsAvailable => AssignmentHistories.All(x => x.ReturnedDate != null); 
    // calculate property, kitap mevcut mu değil mi kontrol eder
    // calismasi icin Book tablosuna atilan sorgularda AssignementHistory tablosunun Include() ile dahil edilmesi gerekir
    // eger include edilmezse history kayitlari gelmez ve IsAvailable her zaman true olur, bu da yanlis bir bilgi verir

    // Navigation Props:
    public ICollection<AssignmentHistory> AssignmentHistories { get; set; } = new List<AssignmentHistory>();
}