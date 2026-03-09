using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationController(LibraryService libraryService) : ControllerBase
{
    [HttpPost("assign")]
    public async Task<IActionResult> AssignBook(Guid bookId, Guid studentId)
    {
        await libraryService.AssignBookToStudentAsync(bookId, studentId);
        return Ok("Kitap öğrenciye verildi");
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook(Guid bookId)
    {
        await libraryService.ReturnBookAsync(bookId);
        return Ok("Kitap iade edildi");
    }
}