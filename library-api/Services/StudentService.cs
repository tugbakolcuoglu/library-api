using WebApplication2.Data;
using WebApplication2.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTOs;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class StudentService (IStudentRepository studentRepository) : IStudentService
{
    
    public async Task<StudentDto?> CreateStudentAsync(StudentDto student)
    {
        var entity = new Student()
        {
            Name = student.Name,
            Surname = student.Surname,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber
        };
        
        var addedEntity = await studentRepository.RegisterNewStudentAsync(entity);
        if(addedEntity.Id != Guid.Empty)
        {
            return new StudentDto
            {
                Name = addedEntity.Name,
                Surname = addedEntity.Surname,
                Email = addedEntity.Email,
                PhoneNumber = addedEntity.PhoneNumber
            };
        }
        
        return null;
        
    }
    
    // private readonly AppDbContext _dbContext; 
    //
    // public StudentService(AppDbContext dbContext)
    // {
    //     _dbContext = dbContext; 
    // }
    //
    // public async Task<Student> CreateStudentAsync(Student student)
    // {
    //     student.Id = Guid.NewGuid();
    //     await _dbContext.Students.AddAsync(student); 
    //     await _dbContext.SaveChangesAsync();         
    //     return student;
    // }
    //
    // public async Task<Student?> LoginAsync(string email, string password)
    // {
    //     return await _dbContext.Students
    //         .FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
    // }
    //
    // public async Task<List<Student>> GetAllStudentsAsync()
    // {
    //     return await _dbContext.Students.ToListAsync();
    // }
   
}