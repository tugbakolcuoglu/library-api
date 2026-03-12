using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class StudentRepository(AppDbContext dbContext) : IStudentRepository
{
    public Task<List<Student>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Student?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Student?> GetByIdWithHistoryAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}