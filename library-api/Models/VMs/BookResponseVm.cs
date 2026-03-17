namespace WebApplication2.Models.VMs;

public class BookResponseVm
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsAvailable { get; set; }
}

public class CreateBookRequestVm
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}

public class UpdateBookRequestVm
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}


public class BookDetailResponseVm
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public List<BookHistoryItemVm> History { get; set; } = new();
}

public class BookHistoryItemVm
{
    public Guid AssignmentHistoryId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentFullName { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}