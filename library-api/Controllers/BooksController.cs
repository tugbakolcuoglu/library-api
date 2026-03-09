using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Services;
namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryService _libraryService;

    public BooksController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet] 
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _libraryService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignBookToStudent([FromBody] AssignDto assignDto)
    {
        try
        {
            await _libraryService.AssignBookToStudentAsync(assignDto.BookId, assignDto.StudentId);
            return Ok("Book assigned successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("return/{bookId}")]
    public async Task<IActionResult> ReturnBook(Guid bookId)
    {
        try
        {
            await _libraryService.ReturnBookAsync(bookId);
            var book = await _libraryService.GetBookByIdAsync(bookId);
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
            return Ok(bookDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            return BadRequest("Title ve Author boş olamaz.");

        var createdBook = await _libraryService.CreateBookAsync(book);
        return Ok(createdBook);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var result = await _libraryService.DeleteBookAsync(id);
        if (!result)
            return NotFound("Kitap bulunamadı.");

        return Ok("Kitap silindi.");
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
    {
        var updatedBook = await _libraryService.UpdateBookAsync(id, book);
        if (updatedBook == null)
            return NotFound("Kitap bulunamadı.");

        return Ok(updatedBook);
    }
    
    [HttpGet("find-by-name")]
    public async Task<IActionResult> FindBooksByName([FromQuery] string name)
    {
        var books = await _libraryService.FindBooksByNameAsync(name);
        return Ok(books);
    }

    [HttpGet("find-by-author")]
    public async Task<IActionResult> FindBooksByAuthorName([FromQuery] string authorName)
    {
        var books = await _libraryService.FindBooksByAuthorNameAsync(authorName);
        return Ok(books);
    }
}