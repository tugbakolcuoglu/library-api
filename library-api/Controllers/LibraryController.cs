using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController(ILibraryService libraryService) : ControllerBase
{
    
}