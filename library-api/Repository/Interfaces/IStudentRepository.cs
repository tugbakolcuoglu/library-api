using WebApplication2.Entities;

namespace WebApplication2.Repository.Interfaces;

public interface IStudentRepository
{
    Task<Student> RegisterNewStudentAsync(Student student);
}