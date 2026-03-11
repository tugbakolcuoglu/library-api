using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

// // TODO: student repo ve servisteki metodlar implemente edildikten sonra bu sınıfı da implemente edin
public class LibraryService(IBookRepository bookRepository, IStudentRepository studentRepository) : ILibraryService
{
    public async Task<bool> AssignBookToStudentAsync(Guid bookId, Guid studentId)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId);
        if (book == null || book.StudentId != null) 
        {
            return false;
        }

        var student = await studentRepository.GetStudentByIdAsync(studentId);
        if (student == null) 
        {
            return false;
        }

        book.StudentId = studentId;

        return await bookRepository.UpdateBookAsync(book);
    }

    public async Task<bool> ReturnBookAsync(Guid bookId)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId);
        if (book == null || book.StudentId == null)
        {
            return false;
        }

        book.StudentId = null;

        return await bookRepository.UpdateBookAsync(book);
    }

    public async Task<List<BookDto>> GetStudentBooksAsync(Guid studentId)
    {
        var books = await bookRepository.GetBooksByStudentIdAsync(studentId);

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author
        }).ToList();
    }
}