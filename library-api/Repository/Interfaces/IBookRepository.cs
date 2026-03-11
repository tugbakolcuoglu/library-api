using WebApplication2.Entities;

namespace WebApplication2.Repository.Interfaces;

/// <summary>
/// "Kitap" nesneleri uzerinde CRUD islemlerini tanimlayan arayuz.
/// </summary>
public interface IBookRepository
{
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<List<Book>> GetAllBooksAsync();
    Task<List<Book>> FindBooksByNameAsync(string name);
    Task<List<Book>> FindBooksByAuthorNameAsync(string author);
    Task<Book> CreateBookAsync(Book book);
    Task<bool> DeleteBookAsync(Guid id);
    Task<bool> UpdateBookAsync(Book updatedBook);
    Task<List<Book>> GetBooksByStudentIdAsync(Guid studentId);
}