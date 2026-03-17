using AutoMapper;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Models.DTOs;
using WebApplication2.Models.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService(IStudentRepository studentRepository, IMapper mapper) : IStudentService
{
    public async Task<List<StudentDto>> GetAllAsync()
    {
        // Student -> StudentDto mapping yapilacak

        var students = await studentRepository.GetAllAsync();

        var studentDtos = mapper.Map<List<StudentDto>>(students);
        return studentDtos;
    }

    public async Task<List<StudentDto>> GetByEmailAsync(string email)
    {
        var students = await studentRepository.GetByEmailAsync(email);

        return mapper.Map<List<StudentDto>>(students);
    }//GetAllAsync metodunun mantığıyla aynı sadece email filterleme yapılıyor, gerisi aynı

    public async Task<StudentDetailDto?> GetByIdAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null) return null;

        var studentDetailDto = mapper.Map<StudentDetailDto>(student);
        return studentDetailDto;
    } // GetByIdAsync metodunda, öğrenci bulunamazsa null döndürüyoruz. Bulunursa detaylı bir DTO oluşturup döndürüyoruz.

    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        var newStudentEntity = mapper.Map<Student>(dto);

        await studentRepository.AddAsync(newStudentEntity);
        return mapper.Map<StudentDto>(newStudentEntity);
    }//gelen create DTO'sunu Student entitysine dönüştürüyoruz, veritabanına ekliyoruz, sonra eklenen entity'yi tekrar DTO'ya dönüştürüp döndürüyoruz

    public async Task<StudentDto?> UpdateAsync(UpdateStudentDto dto)
    {
        var updateStudent = await studentRepository.GetByIdAsync(dto.Id);

        if (updateStudent == null) return null;

        mapper.Map(dto, updateStudent);

        await studentRepository.UpdateAsync(updateStudent);

        return mapper.Map<StudentDto>(updateStudent);
    }//önce öğrenci buluyor yoksa null dönüyor, varsa gelen update DTO'sundaki bilgileri mevcut öğrenci entitysine mapliyoruz,
     //sonra güncellenmiş entity'yi tekrar DTO'ya dönüştürüp döndürüyoruz

    public async Task<bool> DeleteAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);

        if (student == null) return false;

        await studentRepository.DeleteAsync(student);

        return true;
    }//öğrenci bulunamazsa false döndürüyoruz, bulunursa siliyoruz ve true döndürüyoruz
}