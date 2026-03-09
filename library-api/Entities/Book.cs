namespace WebApplication2.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public Guid? StudentId { get; set; }
    
    // Navigation Props:
    public Student? Student { get; set; }
    public ICollection<AssignmentHistory> AssignmentHistories { get; set; } = new List<AssignmentHistory>();
}