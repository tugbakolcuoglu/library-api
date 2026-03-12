namespace WebApplication2.DTOs;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsAvailable { get; set; }
}

public class BookDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public List<BookHistoryItemDto> History { get; set; } = new();
}

public class BookHistoryItemDto
{
    public Guid AssignmentHistoryId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}

public class CreateBookDto
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}

public class UpdateBookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}

