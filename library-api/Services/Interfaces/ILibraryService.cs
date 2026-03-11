using WebApplication2.DTOs;

namespace WebApplication2.Services.Interfaces;

/// <summary>
/// Kitap odunc alma, geri verme gibi kutuphanenin gunluk operasyonlarini yurutmek icin kullanilacak servis arayuzu.
/// </summary>
public interface ILibraryService
{
    /// <summary>
    /// Id si verilen kitabi ogrenciye odunc verir. Kitap zaten odunc verilmis ise false doner, basarili ise true doner.
    /// </summary>
    /// <param name="bookId">Odunc Verilecek Kitap</param>
    /// <param name="studentId">Odunc alan ogrenci</param>
    /// <returns></returns>
    Task<bool> AssignBookToStudentAsync(Guid bookId, Guid studentId);
    
    /// <summary>
    /// Idsi verilen kitabi geri alir. Kitap zaten geri verilmis ise false doner, basarili ise true doner.
    /// </summary>
    /// <param name="bookId">Iade edilen kitap</param>
    /// <returns></returns>
    Task<bool> ReturnBookAsync(Guid bookId);
    
    /// <summary>
    /// idsi verilen ogrencinin odunc aldigi kitaplari dondurur.
    /// </summary>
    /// <param name="studentId">sorgulanacak ogrenci</param>
    /// <returns></returns>
    Task<List<BookDto>> GetStudentBooksAsync(Guid studentId);
    
    
}