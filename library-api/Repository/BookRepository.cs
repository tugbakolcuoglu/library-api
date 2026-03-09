using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;

namespace WebApplication2.Repository;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        return await context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await context.Books
            .Include(b => b.Student)
            .Include(b => b.AssignmentHistories)
            .ThenInclude(h => h.Student)
            .ToListAsync();
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        book.Id = Guid.NewGuid();
        book.IsAvailable = true;

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();

        return book;
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        var book = await context.Books.FindAsync(id);

        if (book == null)
            return false;

        context.Books.Remove(book);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Book?> UpdateBookAsync(Guid id, Book updatedBook)
    {
        var book = await context.Books.FindAsync(id);

        if (book == null)
            return null;

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;

        await context.SaveChangesAsync();

        return book;
    }

    public Task AssignBookToStudentAsync(Guid bookId, Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task ReturnBookAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> FindBooksByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> FindBooksByAuthorNameAsync(string author)
    {
        throw new NotImplementedException();
    }
}