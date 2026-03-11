using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Student>> GetStudentsByNameAsync(string name)
    {
        return await dbContext.Students.Where(s => s.Name.Contains(name)).ToListAsync();
    }
    
    public async Task<Student?> GetStudentByIdAsync(Guid id)
    {
        return await dbContext.Students.FindAsync(id);
    }
}