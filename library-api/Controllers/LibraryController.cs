using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController(LibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await libraryService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        var result = await libraryService.CreateBookAsync(book);
        return Ok(result);
    }
}