using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    public async Task<List<Student>> GetAllAsync()
    {
        return await dbContext.Students.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await dbContext.Students
            .Include(s => s.AssignmentHistories)
                .ThenInclude(s => s.Book)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Student>> GetByEmailAsync(string email)
    {
        return await dbContext.Students
            .Where(s => s.Email.Contains(email.Trim()))
            .ToListAsync();
    }

    public async Task AddAsync(Student student)
    {
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Student student)
    {
        dbContext.Students.Update(student);
        return await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        dbContext.Students.Remove(student);
        await dbContext.SaveChangesAsync();
    }
}