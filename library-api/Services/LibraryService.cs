using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebApplication2.Services;

public class LibraryService
{
    private readonly AppDbContext _context;

    public LibraryService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Book?> GetBookByIdAsync(Guid bookId)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        var list = await _context.Books
            .Include(b => b.Student)
            .Include(b => b.AssignmentHistories)
            .ThenInclude(h => h.Student)
            .ToListAsync();

        return list;
    }
    public async Task AssignBookToStudentAsync(Guid bookId, Guid studentId)
    {
        var book = await _context.Books.FindAsync(bookId);
        var student = await _context.Students.FindAsync(studentId);
        if (book == null || student == null || !book.IsAvailable)
            throw new InvalidOperationException("Book not available or student missing");

        book.IsAvailable = false;
        book.StudentId = studentId;

        var history = new AssignmentHistory
        {
            Id = Guid.NewGuid(),
            BookId = bookId,
            StudentId = studentId,
            AssignedDate = DateTime.UtcNow
        };
        _context.AssignmentHistories.Add(history);
        await _context.SaveChangesAsync();
    }//Kitap atama işlemi için kitap ve öğrenci bilgilerini veritabanından getirir, uygunluk kontrolü yapar,
     //atama işlemini gerçekleştirir ve atama geçmişine kaydeder.

    public async Task ReturnBookAsync(Guid bookId)
    {
        var book = await _context.Books.FindAsync(bookId);

        if (book == null || book.IsAvailable)
        {
            throw new InvalidOperationException("Book is not currently assigned.");
        }

        book.IsAvailable = true;
        book.StudentId = null;

        var assignmentHistory = await _context.AssignmentHistories
            .Where(h => h.BookId == bookId && h.returneDate == default)
            .FirstOrDefaultAsync();

        if (assignmentHistory != null)
        {
            assignmentHistory.returneDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }//Kitap iade işlemi için kitap bilgilerini veritabanından getirir, uygunluk kontrolü yapar,atama
     //geçmişi kaydı oluşturur ve kitap-öğrenci ilişkisinin kaldırılmasını sağlar.

    public async Task<Book> CreateBookAsync(Book book)
    {
        book.Id = Guid.NewGuid(); 
        book.IsAvailable = true;
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    public async Task<bool> DeleteBookAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Book?> UpdateBookAsync(Guid id, Book updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return null;
        
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;

        await _context.SaveChangesAsync();
        return book;
    }
    
    public async Task<List<Book>> FindBooksByNameAsync(string name)
    {
        return await _context.Books
            .Where(b => b.Title.Contains(name)) 
            .Include(b => b.Student) 
            .ToListAsync();
    }


    public async Task<List<Book>> FindBooksByAuthorNameAsync(string authorName)
    {
        return await _context.Books
            .Where(b => b.Author.Contains(authorName))
            .Include(b => b.Student)
            .ToListAsync();
    }
}