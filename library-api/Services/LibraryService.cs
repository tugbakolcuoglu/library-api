using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class LibraryService(IBookRepository bookRepository, IStudentRepository studentRepository) : ILibraryService
{
    // Doldurmayi dene, cok zorlanirsan birak
    public Task<LoanResultDto> BorrowBookAsync(BorrowBookDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<LoanResultDto> ReturnBookAsync(ReturnBookDto dto)
    {
        throw new NotImplementedException();
    }
}