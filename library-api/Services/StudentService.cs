using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto?> CreateStudentAsync(StudentDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Surname = studentDto.Surname,
            Email = studentDto.Email,
            PhoneNumber = studentDto.PhoneNumber
        };

        var created = await _studentRepository.RegisterNewStudentAsync(student);

        return new StudentDto
        {
            Name = created.Name,
            Surname = created.Surname,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber
        };
    }
}