using WebApplication2.DTOs;

namespace WebApplication2.Services.Interfaces;

/// <summary>
/// Öğrenci işlemlerini yöneten servis arayüzü.
/// </summary>
public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<List<StudentDto>> GetByEmailAsync(string email);
    Task<StudentDetailDto?> GetByIdAsync(Guid id);
    Task<StudentDto> CreateAsync(CreateStudentDto dto);
    Task<StudentDto?> UpdateAsync(UpdateStudentDto dto);
    Task<bool> DeleteAsync(Guid id);
}