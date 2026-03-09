using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(LibraryService libraryService) : ControllerBase
{
    // Tüm kitapları getir
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await libraryService.GetAllBooksAsync();
        return Ok(books);
    }

    // Id ile kitap getir
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await libraryService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound("Kitap bulunamadı");
        return Ok(book);
    }

    // Kitap ekle
    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        var createdBook = await libraryService.CreateBookAsync(book);
        return Ok(createdBook);
    }

    // Kitap güncelle
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, Book updatedBook)
    {
        var book = await libraryService.UpdateBookAsync(id, updatedBook);
        if (book == null)
            return NotFound("Kitap bulunamadı");
        return Ok(book);
    }

    // Kitap sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var result = await libraryService.DeleteBookAsync(id);
        if (!result)
            return NotFound("Kitap bulunamadı");
        return Ok("Kitap silindi");
    }

    // Kitap isme göre ara
    [HttpGet("search/name")]
    public async Task<IActionResult> FindBooksByName([FromQuery] string name)
    {
        var books = await libraryService.FindBooksByNameAsync(name);
        return Ok(books);
    }

    // Kitap yazara göre ara
    [HttpGet("search/author")]
    public async Task<IActionResult> FindBooksByAuthor([FromQuery] string author)
    {
        var books = await libraryService.FindBooksByAuthorNameAsync(author);
        return Ok(books);
    }

    // Kitap ödünç ver
    [HttpPost("assign")]
    public async Task<IActionResult> AssignBook(Guid bookId, Guid studentId)
    {
        try
        {
            await libraryService.AssignBookToStudentAsync(bookId, studentId);
            return Ok("Kitap öğrenciye verildi");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Kitap iade
    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook(Guid bookId)
    {
        try
        {
            await libraryService.ReturnBookAsync(bookId);
            return Ok("Kitap iade edildi");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}