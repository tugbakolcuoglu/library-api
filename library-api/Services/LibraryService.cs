using WebApplication2.DTOs;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

// // TODO: student repo ve servisteki metodlar implemente edildikten sonra bu sınıfı da implemente edin
public class LibraryService(IBookRepository bookRepository, IStudentRepository studentRepository) : ILibraryService
{
    public Task<bool> AssignBookToStudentAsync(Guid bookId, Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReturnBookAsync(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Task<List<BookDto>> GetStudentBooksAsync(Guid studentId)
    {
        throw new NotImplementedException();
    }
}