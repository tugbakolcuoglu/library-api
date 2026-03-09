using WebApplication2.Data;
using WebApplication2.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services;

public class StudentService
{
    private readonly AppDbContext _dbContext; 

    public StudentService(AppDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        student.Id = Guid.NewGuid();
        await _dbContext.Students.AddAsync(student); 
        await _dbContext.SaveChangesAsync();         
        return student;
    }
    
    public async Task<Student?> LoginAsync(string email, string password)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }
}