using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService 
{
    public Task<List<StudentDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StudentDetailDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto?> UpdateAsync(UpdateStudentDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}