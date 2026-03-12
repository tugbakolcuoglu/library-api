

using WebApplication2.DTOs;

namespace WebApplication2.Services.Interfaces;

/// <summary>
/// Kitap işlemlerini yöneten servis arayüzü.
/// </summary>
public interface IBookService
{
    Task<List<BookDto>> GetAllAsync();
    Task<BookDetailDto?> GetByIdAsync(Guid id);
    Task<BookDto> CreateAsync(CreateBookDto dto);
    Task<BookDto?> UpdateAsync(UpdateBookDto dto);
    Task<bool> DeleteAsync(Guid id);
}