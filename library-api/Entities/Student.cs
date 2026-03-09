namespace WebApplication2.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Book? Book { get; set; }
    public ICollection<AssignmentHistory> AssignmentHistory { get; set; }
}