using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        return books.Select(b => new BookDto
        {
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return null;

        return new BookDto
        {
            Title = book.Title,
            Author = book.Author,
            IsAvailable = book.IsAvailable,
            StudentId = book.StudentId
        };
    }

    public async Task<BookDto> CreateBookAsync(BookCreateDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author,
            IsAvailable = true
        };

        var createdBook = await _bookRepository.CreateBookAsync(book);

        return new BookDto
        {
            Title = createdBook.Title,
            Author = createdBook.Author,
            IsAvailable = createdBook.IsAvailable,
            StudentId = createdBook.StudentId
        };
    }

    public async Task<BookDto?> UpdateBookAsync(Guid id, BookCreateDto updatedBook)
    {
        var book = await _bookRepository.UpdateBookAsync(id, new Book
        {
            Title = updatedBook.Title,
            Author = updatedBook.Author
        });

        if (book == null) return null;

        return new BookDto
        {
            Title = book.Title,
            Author = book.Author,
            IsAvailable = book.IsAvailable,
            StudentId = book.StudentId
        };
    }

    public async Task<bool> DeleteBookAsync(Guid id)
        => await _bookRepository.DeleteBookAsync(id);

    public async Task AssignBookToStudentAsync(Guid bookId, Guid studentId)
        => await _bookRepository.AssignBookToStudentAsync(bookId, studentId);

    public async Task ReturnBookAsync(Guid bookId)
        => await _bookRepository.ReturnBookAsync(bookId);

    public async Task<List<BookDto>> FindBooksByNameAsync(string name)
    {
        var books = await _bookRepository.FindBooksByNameAsync(name);
        return books.Select(b => new BookDto
        {
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }

    public async Task<List<BookDto>> FindBooksByAuthorNameAsync(string author)
    {
        var books = await _bookRepository.FindBooksByAuthorNameAsync(author);
        return books.Select(b => new BookDto
        {
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }
}