namespace WebApplication2.DTOs;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public Guid? StudentId { get; set; }
}