using WebApplication2.Entities;

namespace WebApplication2.Repository.Interfaces;

/// <summary>
/// Kitap entity'si için veri erişim işlemlerini tanımlar.
/// </summary>
public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task AddAsync(Book book);
    Task<int> UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}