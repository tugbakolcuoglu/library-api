using WebApplication2.Entities;

namespace WebApplication2.Repository.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetBookByIdAsync(Guid bookId);
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> CreateBookAsync(Book book);
    Task<bool> DeleteBookAsync(Guid id);
    Task<Book?> UpdateBookAsync(Guid id, Book updatedBook);
    Task AssignBookToStudentAsync(Guid bookId, Guid studentId);
    Task ReturnBookAsync(Guid bookId);
    Task<List<Book>> FindBooksByNameAsync(string name);
    Task<List<Book>> FindBooksByAuthorNameAsync(string author);
}