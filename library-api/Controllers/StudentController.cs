using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await studentService.GetAllAsync();
        // yukardan gelen ogrenci nesnesi, StudentDto tipinde, burda bu nesneyi VM'ye donusturmemiz lazim

        var studentsVm = students.Select(s => new StudentResponseVm()
        {
            Id = s.Id,
            Email = s.Email,
            Name = s.Name,
            Surname = s.Surname,
            PhoneNumber = s.PhoneNumber
        }).ToList();

        return Ok(studentsVm);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null)
            return NotFound();
        
        var studentVm = new StudentResponseVm()
        {
            Id = student.Id,
            Email = student.Email,
            Name = student.Name,
            Surname = student.Surname,
            PhoneNumber = student.PhoneNumber
        };

        return Ok(studentVm);
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetStudentByEmail(string email)
    {
        var students = await studentService.GetByEmailAsync(email);
        if (students == null)
            return NotFound();
        var studentsVm = students.Select(s => new StudentResponseVm()
        {
            Id = s.Id,
            Email = s.Email,
            Name = s.Name,
            Surname = s.Surname,
            PhoneNumber = s.PhoneNumber
        }).ToList();
        return Ok(studentsVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentRequestVm request)  
    {
        var createdStudent = await studentService.CreateAsync(new CreateStudentDto()
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        });

        var studentVm = new StudentResponseVm()
        {
            Id = createdStudent.Id,
            Email = createdStudent.Email,
            Name = createdStudent.Name,
            Surname = createdStudent.Surname,
            PhoneNumber = createdStudent.PhoneNumber
        };

        return CreatedAtAction(nameof(GetStudentById), new { id = studentVm.Id }, studentVm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(UpdateStudentRequestVm request)
    {
        var updatedStudent = await studentService.UpdateAsync(new UpdateStudentDto()
        {
            Id = request.Id,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        });
        return Ok(updatedStudent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var isDeleted = await studentService.DeleteAsync(id);
        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}