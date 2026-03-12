using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService, ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var booksDto = await bookService.GetAllAsync();
        // dto turunde oldugu icin burda bunu VM cevirmemiz gerkiyor
        // cunku frontend DTO'yu bilmez, sadece VM'yi bilir
        
        var booksVms = booksDto.Select(b => new BookResponseVm
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            IsAvailable = b.IsAvailable
        }).ToList(); // View Modele donusturulmus listeyi olusturduk, artik bunu FE 'ye gonderebiliriz
        
        return Ok(booksVms);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var bookDetailDto = await bookService.GetByIdAsync(id);

        if (bookDetailDto == null)
            return NotFound("Kitap bulunamadı");

        var bookDetailVm = new BookDetailResponseVm
        {
            Id = bookDetailDto.Id,
            Title = bookDetailDto.Title,
            Author = bookDetailDto.Author,
            IsAvailable = bookDetailDto.IsAvailable,
            History = bookDetailDto.History.Select(h => new BookHistoryItemVm
            {
                AssignmentHistoryId = h.AssignmentHistoryId,
                StudentId = h.StudentId,
                StudentFullName = h.StudentFullName,
                AssignedDate = h.AssignedDate,
                ReturnedDate = h.ReturnedDate
            }).ToList()
        };
        // az once servis katmaninda yaptigimiz ayni islemi bu sefer DTO -> VM donusumu icin burda yaptik,
        // yine 2 farkli yontemle yapilabilir
        // isterinse history bilgisini BookDetailDto icinde doldurabiliriz, istersek de disarda doldurabiliriz, bu tamamen tercihe bagli

        return Ok(bookDetailVm);
    }
}

