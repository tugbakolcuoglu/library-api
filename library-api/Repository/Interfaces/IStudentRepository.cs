using WebApplication2.Entities;
using WebApplication2.Models.Entities;

namespace WebApplication2.Repository.Interfaces;

/// <summary>
/// Student entity'si için veri erişim işlemlerini tanımlar.
/// </summary>
public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task<List<Student>> GetByEmailAsync(string email);
    Task AddAsync(Student student);
    Task<int> UpdateAsync(Student student);
    Task DeleteAsync(Student student);
}