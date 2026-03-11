using WebApplication2.DTOs;
using WebApplication2.Entities;

namespace WebApplication2.Services.Interfaces;

public interface IBookService
{
    Task<BookDto?> GetBookByIdAsync(Guid id);
    Task<List<BookDto>> GetAllBooksAsync();
    Task<List<BookDto>> FindBooksByNameAsync(string name);
    Task<List<BookDto>> FindBooksByAuthorNameAsync(string author);
    Task<BookDto> CreateBookAsync(BookCreateDto bookDto);
    Task<bool> DeleteBookAsync(Guid id);
    Task<BookDto?> UpdateBookAsync(BookDto updatedBook);
}