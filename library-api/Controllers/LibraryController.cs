using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.DTOs;
using WebApplication2.Services.Interfaces;
using WebApplication2.VMs;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController(ILibraryService libraryService, IMapper mapper ) : ControllerBase
{

    [HttpPost("borrow")]
    public async Task<IActionResult> BorrowBookAsync([FromBody]BorrowBookRequestVm req)
    {
        var requestDto = mapper.Map<BorrowBookDto>(req);
        
        var result = await libraryService.BorrowBookAsync(requestDto);

        if (result != null)
        {
            return Ok(mapper.Map<LoanResultVm>(result));
        }
        
        return BadRequest();
    }
    
    [HttpPost("return")]
    public async Task<IActionResult> ReturnBookAsync([FromBody]ReturnBookRequestVm req)
    {
        var requestDto = mapper.Map<ReturnBookDto>(req);
        
        var result = await libraryService.ReturnBookAsync(requestDto);

        if (result != null)
        {
            return Ok(mapper.Map<LoanResultVm>(result));
        }
        
        return BadRequest();
    }
}