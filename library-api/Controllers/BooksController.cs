using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models.VMs;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService, ILibraryService libraryService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var booksDto = await bookService.GetAllAsync();
        // dto turunde oldugu icin burda bunu VM cevirmemiz gerkiyor
        // cunku frontend DTO'yu bilmez, sadece VM'yi bilir
        
        var booksVms = mapper.Map<List<BookResponseVm>>(booksDto);
        
        return Ok(booksVms);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var bookDetailDto = await bookService.GetByIdAsync(id);

        if (bookDetailDto == null)
            return NotFound("Kitap bulunamadı");

        #region Automaper Olmadan
        // var bookDetailVm = new BookDetailResponseVm
        // {
        //     Id = bookDetailDto.Id,
        //     Title = bookDetailDto.Title,
        //     Author = bookDetailDto.Author,
        //     IsAvailable = bookDetailDto.IsAvailable,
        //     History = bookDetailDto.History.Select(h => new BookHistoryItemVm
        //     {
        //         AssignmentHistoryId = h.AssignmentHistoryId,
        //         StudentId = h.StudentId,
        //         StudentFullName = h.StudentFullName,
        //         AssignedDate = h.AssignedDate,
        //         ReturnedDate = h.ReturnedDate
        //     }).ToList()
        // };
        // // az once servis katmaninda yaptigimiz ayni islemi bu sefer DTO -> VM donusumu icin burda yaptik,
        // // yine 2 farkli yontemle yapilabilir
        // // isterinse history bilgisini BookDetailDto icinde doldurabiliriz, istersek de disarda doldurabiliriz, bu tamamen tercihe bagli
        
        #endregion
        
        
        var bookDetailVm = mapper.Map<BookDetailResponseVm>(bookDetailDto);

        return Ok(bookDetailVm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequestVm request)
    {
        var dtoModel = mapper.Map<CreateBookDto>(request);
        
        var serviceResult = await bookService.CreateAsync(dtoModel);

        return Ok(mapper.Map<BookResponseVm>(serviceResult));
        
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookRequestVm request)
    {
        if(id != request.Id)
            return BadRequest("Id'ler eşleşmiyor");

        var updateRequestDto = mapper.Map<UpdateBookDto>(request);
        
        var serviceResult = await bookService.UpdateAsync(updateRequestDto);

        return Ok(mapper.Map<BookResponseVm>(serviceResult));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var isDeleted = await bookService.DeleteAsync(id);
        if(!isDeleted)
            return NotFound("Kitap bulunamadı veya silinemedi");
        
        return Ok();
    }
}

