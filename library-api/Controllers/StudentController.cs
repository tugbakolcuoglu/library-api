using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models.DTOs;
using WebApplication2.Models.VMs;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await studentService.GetAllAsync();
        // yukardan gelen ogrenci nesnesi, StudentDto tipinde, burda bu nesneyi VM'ye donusturmemiz lazim

        var studentsVm = mapper.Map<List<StudentResponseVm>>(students);

        return Ok(studentsVm);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null)
            return NotFound();

        var studentVm = mapper.Map<StudentDetailResponseVm>(student);

        return Ok(studentVm);
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetStudentByEmail(string email)
    {
        var students = await studentService.GetByEmailAsync(email);

        var studentsVm = mapper.Map<List<StudentResponseVm>>(students);
        return Ok(studentsVm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentRequestVm request)
    {
        var dto = mapper.Map<CreateStudentDto>(request);
        var createdStudent = await studentService.CreateAsync(dto);

        var studentVm = mapper.Map<StudentResponseVm>(createdStudent);
        return Ok(studentVm);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(UpdateStudentRequestVm request)
    {
        var dto = mapper.Map<UpdateStudentDto>(request);
        var updatedStudent = await studentService.UpdateAsync(dto);
        if (updatedStudent == null)
            return NotFound();
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