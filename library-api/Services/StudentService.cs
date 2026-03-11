using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService {
    public async Task<StudentDto?> CreateStudentAsync(StudentDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Surname = studentDto.Surname,
            Email = studentDto.Email,
            PhoneNumber = studentDto.PhoneNumber
        };

        var created = await studentRepository.RegisterNewStudentAsync(student);

        return new StudentDto
        {
            Name = created.Name,
            Surname = created.Surname,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber
        };
    }

    public async Task<List<StudentDto>> FindStudentsByNameAsync(string name)
    {
        var students = await studentRepository.GetStudentsByNameAsync(name);
        
        return students.Select(s => new StudentDto
        {
            Name = s.Name,
            Surname = s.Surname,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber
        }).ToList();
    }
}