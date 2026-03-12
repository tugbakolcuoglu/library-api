namespace WebApplication2.VMs;

public class BorrowBookRequestVm
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
}


public class ReturnBookRequestVm
{
    public Guid BookId { get; set; }
}

public class LoanResultVm
{
    public Guid AssignmentHistoryId { get; set; }
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public bool IsActive { get; set; }
}