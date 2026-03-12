namespace WebApplication2.VMs;

public class StudentResponseVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class CreateStudentRequestVm
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class UpdateStudentRequestVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class StudentDetailResponseVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<StudentHistoryItemVm> History { get; set; } = new();
}

public class StudentHistoryItemVm
{
    public Guid AssignmentHistoryId { get; set; }
    public Guid BookId { get; set; }
    public string BookTitle { get; set; } = null!;
    public string BookAuthor { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}