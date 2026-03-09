using WebApplication2.Data;
using WebApplication2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
    {
        if (
            string.IsNullOrEmpty(studentDto.Email) ||
            string.IsNullOrEmpty(studentDto.Name) ||
            string.IsNullOrEmpty(studentDto.Surname) ||
            string.IsNullOrEmpty(studentDto.PhoneNumber)
        )
        {
            return BadRequest("Email is required");
        }

        var response = await studentService.CreateStudentAsync(studentDto);

        if (response is not null) return Ok();
        
        return BadRequest("Student could not be created");
    }


    // private readonly StudentService _studentService;
    //
    // public StudentController(StudentService studentService)
    // {
    //     _studentService = studentService;
    // }
    //

    //
    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] StudentLoginDto loginDto)
    // {
    //     var student = await _studentService.LoginAsync(loginDto.Email, loginDto.Password);
    //     if (student == null)
    //         return BadRequest("Email veya şifre yanlış");
    //     return Ok(student);
    // }
    //
    // [HttpGet("list")]
    // public async Task<IActionResult> GetAllStudents()
    // {
    //     var students = await _studentService.GetAllStudentsAsync();
    //     return Ok(students);
    // }
}