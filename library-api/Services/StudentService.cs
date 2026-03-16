using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Models.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService 
{
    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await studentRepository.GetAllAsync();
        
        return students.Select(s => new StudentDto 
        { 
            Id = s.Id, 
            Name = s.Name, 
            Surname = s.Surname, 
            Email = s.Email 
        }).ToList();
    }

    public async Task<List<StudentDto>> GetByEmailAsync(string email)
    {
        var students = await studentRepository.GetByEmailAsync(email);
        
        return students.Select(s => new StudentDto 
        { 
            Id = s.Id, 
            Name = s.Name, 
            Surname = s.Surname, 
            Email = s.Email 
        }).ToList();
    }

    public async Task<StudentDetailDto?> GetByIdAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);
        
        if (student == null) return null;

        return new StudentDetailDto
        {
            Id = student.Id,
            Name = student.Name,
            Surname = student.Surname,
            Email = student.Email
        };
    }// GetByIdAsync metodunda, öğrenci bulunamazsa null döndürüyoruz. Bulunursa detaylı bir DTO oluşturup döndürüyoruz.

    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        var student = new Student
        {
            Id = Guid.NewGuid(), // Yeni bir GUID oluşturuyoruz
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email
        };

        await studentRepository.AddAsync(student);

        return new StudentDto 
        { 
            Id = student.Id, 
            Name = student.Name, 
            Surname = student.Surname, 
            Email = student.Email 
        };
    }

    public async Task<StudentDto?> UpdateAsync(UpdateStudentDto dto)
    {
        var student = await studentRepository.GetByIdAsync(dto.Id);
        
        if (student == null) return null;

        // Mevcut entity'yi DTO'dan gelen bilgilerle güncelliyoruz
        student.Name = dto.Name;
        student.Surname = dto.Surname;
        student.Email = dto.Email;

        await studentRepository.UpdateAsync(student);


        return new StudentDto 
        { 
            Id = student.Id, 
            Name = student.Name, 
            Surname = student.Surname, 
            Email = student.Email 
        };// Güncellenmiş bilgileri DTO olarak döndürüyoruz
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);
        
        if (student == null) return false;// Silinecek öğrenci bulunamazsa false döndürüyoruz

        await studentRepository.DeleteAsync(student);
        
        return true;
    }
}