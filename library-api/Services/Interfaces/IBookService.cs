using WebApplication2.DTOs;
using WebApplication2.Entities;

namespace WebApplication2.Services.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(Guid id);
    Task<BookDto> CreateBookAsync(BookCreateDto bookDto);
    Task<BookDto?> UpdateBookAsync(Guid id, BookCreateDto updatedBook);
    Task<bool> DeleteBookAsync(Guid id);

    Task AssignBookToStudentAsync(Guid bookId, Guid studentId);
    Task ReturnBookAsync(Guid bookId);

    Task<List<BookDto>> FindBooksByNameAsync(string name);
    Task<List<BookDto>> FindBooksByAuthorNameAsync(string author);
}