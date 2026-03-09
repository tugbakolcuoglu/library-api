using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    public async Task<Student> RegisterNewStudentAsync(Student student)
    {
        student.Id = Guid.NewGuid();
        
        await dbContext.AddAsync(student);
        await dbContext.SaveChangesAsync();
        
        return student;
    }
}