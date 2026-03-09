using WebApplication2.Data;
using WebApplication2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;

namespace WebApplication2.Services;

[ApiController]
[Route("api/[controller]")]
public class StudentController: ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
    {
        if (string.IsNullOrEmpty(studentDto.Username) || string.IsNullOrEmpty(studentDto.Email))
        {
            return BadRequest("Öğrenci adı ve email boş bırakılamaz.");
        }

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = studentDto.Username,
            Email = studentDto.Email
        };

        var createdStudent = await _studentService.CreateStudentAsync(student);
        return Ok(createdStudent);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] StudentLoginDto loginDto)
    {
        var student = await _studentService.LoginAsync(loginDto.Email, loginDto.Password);
        if (student == null)
            return BadRequest("Email veya şifre yanlış");
        return Ok(student);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }
}