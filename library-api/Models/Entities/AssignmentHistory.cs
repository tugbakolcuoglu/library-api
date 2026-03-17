using WebApplication2.Entities;

namespace WebApplication2.Models.Entities;

public class AssignmentHistory
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }

    //nap Props:
    public Book Book { get; set; } = null!;
    public Student Student { get; set; } = null!;
}