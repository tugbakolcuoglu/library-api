using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<List<BookDto>> GetAllAsync()
    {
        var bookEntities = await bookRepository.GetAllAsync();
        
        // Book entitysini BookDto'ya dönüştür
        var bookDtos = bookEntities.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            IsAvailable = book.IsAvailable
        }).ToList();
        
        return bookDtos;

    }

    public async Task<BookDetailDto?> GetByIdAsync(Guid id)
    {
        var bookEntity = await bookRepository.GetByIdAsync(id);
        
        if (bookEntity == null)
            return null;
        
        // Book entitysini BookDetailDto'ya dönüştür

        #region 1. Yontem -- History bilgisini icerde doldurmak
        
        var bookDetailDto = new BookDetailDto
        {
            Id = bookEntity.Id,
            Title = bookEntity.Title,
            Author = bookEntity.Author,
            IsAvailable = bookEntity.IsAvailable,
            History = bookEntity.AssignmentHistories.Select(x=> new BookHistoryItemDto()
            {
                AssignmentHistoryId = x.Id,
                StudentId = x.StudentId,
                StudentFullName = $"{x.Student.Name} {x.Student.Surname}",
                AssignedDate = x.AssignedDate,
                ReturnedDate = x.ReturnedDate
            }).ToList()
        };


        #endregion
     

        #region 2.Yontem -- History bilgisini ayrica disarda doldurmak
        
        // var bookDetailDto = new BookDetailDto
        // {
        //     Id = bookEntity.Id,
        //     Title = bookEntity.Title,
        //     Author = bookEntity.Author,
        //     IsAvailable = bookEntity.IsAvailable
        // };
        // // once ana bilgileri doldurduk, sonra history bilgisini dolduracagiz
        //
        // var historyDtos = bookEntity.AssignmentHistories.Select(x=> new BookHistoryItemDto()
        // {
        //     AssignmentHistoryId = x.Id,
        //     StudentId = x.StudentId,
        //     StudentFullName = $"{x.Student.Name} {x.Student.Surname}",
        //     AssignedDate = x.AssignedDate,
        //     ReturnedDate = x.ReturnedDate
        // }).ToList();
        //
        // // historyDtos listesini bookDetailDto'nun History property'sine atiyoruz
        //
        // bookDetailDto.History = historyDtos;

        #endregion

        // NOT her iki yontemde de LINQ kullanarak ve ya istenirse foreach dongusu ile tek tek olusturlabilir,
        // Ben LINQ tercih ettim cunku daha kisa ve okunabilir oluyor, ama bu tamamen tercihe baglidir.
        
        
        return bookDetailDto; // olusturulan dto dondurulur
    }

    public Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto?> UpdateAsync(UpdateBookDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}