using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController(ILibraryService libraryService) : ControllerBase
{
    [HttpPost("assign")]
    public async Task<IActionResult> AssignBook(Guid bookId, Guid studentId)
    {
        var result = await libraryService.AssignBookToStudentAsync(bookId, studentId);

        if (!result)
            return BadRequest("Kitap atanamadı");

        return Ok("Kitap öğrenciye verildi");
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook(Guid bookId)
    {
        var result = await libraryService.ReturnBookAsync(bookId);

        if (!result)
            return BadRequest("Kitap iade edilemedi");

        return Ok("Kitap iade edildi");
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentBooks(Guid studentId)
    {
        var books = await libraryService.GetStudentBooksAsync(studentId);
        return Ok(books);
    }
}