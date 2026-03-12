using WebApplication2.Entities;

namespace WebApplication2.Repository.Interfaces;

/// <summary>
/// Student entity'si için veri erişim işlemlerini tanımlar.
/// </summary>
public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task<Student?> GetByIdWithHistoryAsync(Guid id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
    Task<bool> ExistsAsync(Guid id);
    Task SaveChangesAsync();
}