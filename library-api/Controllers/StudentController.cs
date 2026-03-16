using Microsoft.AspNetCore.Mvc;
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
    
    // TODO: Implement other endpoints of 
    // [HttpGet]GetByEmailASyn,
    // [HttpPost]CreateASync,
    // [HttpPut]UpdateAsync,
    // [HttpDelete]DeleteAsync
}