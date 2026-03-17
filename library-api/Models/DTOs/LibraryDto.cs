namespace WebApplication2.Models.DTOs;

public class BorrowBookDto
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
}

public class ReturnBookDto
{
    public Guid BookId { get; set; }
}

public class LoanResultDto
{
    public Guid AssignmentHistoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public bool IsActive { get; set; }
}