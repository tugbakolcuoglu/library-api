using WebApplication2.DTOs;

namespace WebApplication2.Services.Interfaces;

public interface IStudentService
{
    Task<StudentDto?> CreateStudentAsync(StudentDto student);
}