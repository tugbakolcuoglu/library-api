using System.ComponentModel.DataAnnotations;
using WebApplication2.Entities;

namespace WebApplication2.Models.Entities;

public class Student
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    [MaxLength(50)]
    public string Surname { get; set; } = null!;
    
    [MaxLength(12)]
    public string PhoneNumber { get; set; } = null!;
    
    [MaxLength(150)]
    public string Email { get; set; } = null!;
    
    // nav prop:
    public ICollection<AssignmentHistory> AssignmentHistories { get; set; } = new List<AssignmentHistory>();
}