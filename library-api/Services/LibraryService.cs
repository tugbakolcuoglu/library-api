using AutoMapper;
using WebApplication2.Models.DTOs;
using WebApplication2.Models.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class LibraryService(
    IBookRepository bookRepository, 
    IStudentRepository studentRepository, 
    IAssignmentHistoryRepository historyRepository,
    IMapper mapper) : ILibraryService
{
    public async Task<LoanResultDto?> BorrowBookAsync(BorrowBookDto dto)
    {
        // - kitap id'den kitabu bul
        var book = await bookRepository.GetByIdAsync(dto.BookId); 
        if (book == null) return null;

        // - ogrenciyi bul
        var student = await studentRepository.GetByIdAsync(dto.StudentId); // guncelleme islemini student isimli degisken uzerinden yapacagiz
        if (student == null) return null;

        // -kitabi ogrenci ile iliskilendir 
        var assignmentHistory = new AssignmentHistory()
        {
            Id = Guid.NewGuid(),
            BookId = book.Id,
            StudentId = student.Id,
            AssignedDate = DateTime.UtcNow,
            ReturnedDate = null,
        };
        
        var result = await historyRepository.CreateHistoryAsync(assignmentHistory); // history kaydini olusturduk
        
        return result == null ? null : mapper.Map<LoanResultDto>(result);
    }

    public async Task<LoanResultDto?> ReturnBookAsync(ReturnBookDto dto)
    {
        var book = await bookRepository.GetByIdAsync(dto.BookId);

        if (book == null) return null;

        // - ilgili history kayidini bulcaz,
        var history = book.AssignmentHistories.FirstOrDefault(x => x.ReturnedDate == null);
        if (history == null) return null;

        history.ReturnedDate = DateTime.UtcNow; // kitabin iade tarihini guncelliyoruz
        
        var result = await historyRepository.UpdateHistoryAsync(history); // degisikligi kaydet

        return result == null ? null : mapper.Map<LoanResultDto>(result); // basariliysa mapper ile maple ve don, degilse null don
        
    }
}