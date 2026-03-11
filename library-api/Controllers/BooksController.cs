using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService, ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await bookService.GetBookByIdAsync(id);

        if (book == null)
            return NotFound("Kitap bulunamadı");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookCreateDto bookDto)
    {
        var createdBook = await bookService.CreateBookAsync(bookDto);
        return Ok(createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, BookDto updatedBookDto)
    {
        updatedBookDto.Id = id;

        var book = await bookService.UpdateBookAsync(updatedBookDto);

        if (book == null)
            return NotFound("Kitap bulunamadı");

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var result = await bookService.DeleteBookAsync(id);

        if (!result)
            return NotFound("Kitap bulunamadı");

        return Ok("Kitap silindi");
    }

    [HttpGet("search/name")]
    public async Task<IActionResult> FindBooksByName([FromQuery] string name)
    {
        var books = await bookService.FindBooksByNameAsync(name);
        return Ok(books);
    }

    [HttpGet("search/author")]
    public async Task<IActionResult> FindBooksByAuthor([FromQuery] string author)
    {
        var books = await bookService.FindBooksByAuthorNameAsync(author);
        return Ok(books);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignBook(Guid bookId, Guid studentId)
    {
        var result = await libraryService.AssignBookToStudentAsync(bookId, studentId);

        if (!result)
        {
            return BadRequest("Kitap atanamadı. Kitap zaten birinde olabilir veya bilgiler hatalıdır.");
        }

        return Ok("Kitap başarıyla öğrenciye verildi");
    }
}