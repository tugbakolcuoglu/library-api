using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await bookRepository.GetBookByIdAsync(id);
        if (book == null) return null;

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            IsAvailable = book.IsAvailable,
            StudentId = book.StudentId
        };
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var books = await bookRepository.GetAllBooksAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }

    public async Task<List<BookDto>> FindBooksByNameAsync(string name)
    {
        var books = await bookRepository.FindBooksByNameAsync(name);
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }

    public async Task<List<BookDto>> FindBooksByAuthorNameAsync(string author)
    {
        var books = await bookRepository.FindBooksByAuthorNameAsync(author);
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable,
            StudentId = b.StudentId
        }).ToList();
    }


    public async Task<BookDto> CreateBookAsync(BookCreateDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author,
            IsAvailable = true
        };

        var createdBook = await bookRepository.CreateBookAsync(book);

        return new BookDto
        {
            Id =  createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
            IsAvailable = createdBook.IsAvailable,
            StudentId = createdBook.StudentId
        };
    }

    public async Task<BookDto?> UpdateBookAsync(BookDto updatedBook)
    {
        var entityBook = new Book
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            IsAvailable = updatedBook.IsAvailable,
            StudentId = updatedBook.StudentId
        };
        
        var updatedBookEntity = await bookRepository.UpdateBookAsync(entityBook);

        if (updatedBookEntity == null) return null;

        return new BookDto
        {
            Id =  updatedBookEntity.Id,
            Title = updatedBookEntity.Title,
            Author = updatedBookEntity.Author,
            IsAvailable = updatedBookEntity.IsAvailable,
            StudentId = updatedBookEntity.StudentId
        };
    }

    public async Task<bool> DeleteBookAsync(Guid id)
        => await bookRepository.DeleteBookAsync(id);
    
}