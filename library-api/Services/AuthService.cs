using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTOs;
using WebApplication2.Entities;

namespace WebApplication2.Services;

public class AuthService
{
    private readonly AppDbContext _dbContext;

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateStudentAsync(LoginDto loginDto)
    {
        var student = new Student()
        {
            Id = Guid.NewGuid(),
            Name = loginDto.Username, 
            Email = loginDto.Email,
            Password = loginDto.Password,
            Surname = "", 
            PhoneNumber = "" 
        };

        await _dbContext.Students.AddAsync(student); 
        await _dbContext.SaveChangesAsync();
    }//Öğrenci oluşturmak için verilen loginDto nesnesini kullanarak yeni bir Student nesnesi oluşturur ve veritabanına kaydeder.

    public async Task<Student?> RegisterAsync(RegisterDto registerDto)
    {
        var existingStudent = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Email == registerDto.Email);

        if (existingStudent != null)
            return null;

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = registerDto.Username,
            Email = registerDto.Email,
            Password = registerDto.Password,
            Surname = "", 
            PhoneNumber = ""
        };

        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();

        return student;
    }//Kayıt işlemi için verilen email adresinin veritabanında zaten var olup olmadığını kontrol eder.

    public async Task<Student?> LoginAsync(LoginDto loginDto)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(s =>
                s.Email == loginDto.Email &&
                s.Password == loginDto.Password);
    }//Login işlemi için verilen email ve şifreye sahip bir öğrenci olup olmadığını veritabanında kontrol eder. 
    
    public async Task<bool> DeleteStudentAsync(Guid id)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
            return false;

        _dbContext.Students.Remove(student);

        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Student?> UpdateStudentAsync(Guid id, StudentDto studentDto)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
        {
            return null; // Öğrenci yoksa null dön ki Controller 'NotFound' dönebilsin
        }

        // Bilgileri DTO'dan gelenlerle güncelle
        student.Name = studentDto.Username;
        student.Email = studentDto.Email;
        student.Password = studentDto.Password;
        
        await _dbContext.SaveChangesAsync();

        return student;
    }
    
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }
    
    public async Task<List<Student>> FindStudentsByNameAsync(string name)
    {
        // İsim ile arama yapar ve eşleşen öğrencileri döner
        return await _dbContext.Students
            .Where(s => s.Name.Contains(name))
            .ToListAsync();
    }
}