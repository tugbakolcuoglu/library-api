using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Services; 

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    //Servis çağırıyoruz
    private readonly LibraryService _libraryService; 

    public LibraryController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }
    
    [HttpGet("GetAllBooks")]
    public async Task<IActionResult> GetAllBooks()
    {
        // Servisteki metodu çağırıyoruz
        var result = await _libraryService.GetAllBooksAsync(); 
        return Ok(result);
    }

    [HttpPost("AssignBookToStudent")]
    public async Task<IActionResult> AssignBookToStudent(Guid bookId, Guid studentId)
    {
        try
        {
            //AssignBookToStudentAsync çalıştır.
            await _libraryService.AssignBookToStudentAsync(bookId, studentId);
            return Ok("Book assigned successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);//Hata durumunda mesaj döndür.
        }
    }//Kitap atama işlemi için kitapId ve studentId parametreleri alır. Servisteki AssignBookToStudentAsync metodunu çağırır.
     //İşlem başarılıysa "Book assigned successfully." mesajı döndürür, hata durumunda ise hatanın mesajını döndürür.

    [HttpPost("ReturnBookFromStudent")]//Kitap iade işlemi için endpoint
    public async Task<IActionResult> ReturnBookFromStudent(Guid bookId)
        //Kitap iade işlemi için kitapId parametresi alır
    {
        try
        {
            await _libraryService.ReturnBookAsync(bookId);
            //Kitap iade işlemi için servis metodunu çağırır
            return Ok("Book returned successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("assign")] 
    public async Task<IActionResult> AssignBook(Guid bookId, Guid studentId)
    {
        try
        {
            await _libraryService.AssignBookToStudentAsync(bookId, studentId);
            return Ok("Kitap başarıyla atandı.");
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
            return Ok("Kitap başarıyla geri alındı.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("DeleteBook/{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        await _libraryService.DeleteBookAsync(id);
        return Ok("Kitap silindi.");
    }

    // 5. FindBooksByName
    [HttpGet("FindBooksByName")]
    public async Task<IActionResult> FindBooksByName(string name)
    {        var books = await _libraryService.FindBooksByNameAsync(name);
        return Ok(books);
    }
    
    [HttpPut("UpdateBook/{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
    {
        var updatedBook = await _libraryService.UpdateBookAsync(id, book);
        if (updatedBook == null)
            return NotFound("Kitap bulunamadı.");

        return Ok(updatedBook);
    }
}