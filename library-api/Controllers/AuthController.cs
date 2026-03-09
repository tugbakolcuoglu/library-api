using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTOs;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly AuthService _authService; 
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("create-student")]
    public async Task<IActionResult> CreateUser([FromBody] LoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest("Email ve şifre alanları boş bırakılamaz.");
        }

        await _authService.CreateStudentAsync(loginDto);
        return Ok("Kullanıcı başarıyla oluşturuldu. Artık login yapabilirsin!");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = await _authService.RegisterAsync(registerDto);
        if (user == null)
            return BadRequest("Email is already in use.");
        return Ok(user);
    }//Kayıt işlemi için register endpoint'i oluşturulur. Gelen RegisterDto ile AuthService'deki RegisterAsync metodu çağrılır.
     //Eğer kullanıcı oluşturulamazsa (örneğin email zaten kullanılıyorsa) BadRequest döndürülür, aksi halde oluşturulan kullanıcı bilgisi döndürülür.

     [HttpPost("login")]
     public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
     {
         var student = await _authService.LoginAsync(loginDto);
         if (student == null)
         {
             return BadRequest("Invalid email or password.");
         }
         return Ok(new 
         { 
             Message = "Login successful.", 
             Data = student 
         });
     }
     
     [HttpDelete("delete-student/{id}")]
     public async Task<IActionResult> DeleteStudent(Guid id)
     {
         var result = await _authService.DeleteStudentAsync(id);

         if (!result)
         {
             return NotFound("Öğrenci bulunamadı");
         }

         return Ok("Öğrenci başarıyla silindi");
     }
     
     [HttpGet("students")]
     public async Task<IActionResult> GetAllStudents()
     {
         var students = await _authService.GetAllStudentsAsync();

         return Ok(students);
     }
     
     [HttpPut("update-student/{id}")]
     public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentDto studentDto)
     {
         var updatedStudent = await _authService.UpdateStudentAsync(id, studentDto);
         if (updatedStudent == null)
         {
             return NotFound("Öğrenci bulunamadı.");
         }
         return Ok(updatedStudent);
     }

     [HttpGet("find-students")]
     public async Task<IActionResult> FindStudentsByName([FromQuery] string name)
     {
         var students = await _authService.FindStudentsByNameAsync(name);
         return Ok(students);
     }
}
