using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Services;

public class LibraryService
{
    private readonly IBookRepository _bookRepository;

    public LibraryService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        return await _bookRepository.GetBookByIdAsync(bookId);
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllBooksAsync();
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        return await _bookRepository.CreateBookAsync(book);
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        return await _bookRepository.DeleteBookAsync(id);
    }

    public async Task<Book?> UpdateBookAsync(Guid id, Book updatedBook)
    {
        return await _bookRepository.UpdateBookAsync(id, updatedBook);
    }

    public async Task ReturnBookAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public async Task AssignBookToStudentAsync(Guid bookId, Guid studentId)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> FindBooksByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> FindBooksByAuthorNameAsync(string author)
    {
        throw new NotImplementedException();
    }
}