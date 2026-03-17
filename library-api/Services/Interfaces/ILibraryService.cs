
using WebApplication2.Models.DTOs;

namespace WebApplication2.Services.Interfaces;


/// <summary>
/// Kütüphane günlük operasyonlarını yöneten servis arayüzü.
/// </summary>
public interface ILibraryService
{
    Task<LoanResultDto?> BorrowBookAsync(BorrowBookDto dto);
    Task<LoanResultDto?> ReturnBookAsync(ReturnBookDto dto);
}