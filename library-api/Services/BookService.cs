using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Repository.Interfaces;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services;

public class BookService(IBookRepository bookRepository, IMapper mapper) : IBookService
{
    public async Task<List<BookDto>> GetAllAsync()
    {
        var bookEntities = await bookRepository.GetAllAsync();
        
        // Book entitysini BookDto'ya dönüştür
        // var bookDtos = bookEntities.Select(book => new BookDto
        // {
        //     Id = book.Id,
        //     Title = book.Title,
        //     Author = book.Author,
        //     IsAvailable = book.IsAvailable
        // }).ToList();

        var bookDtos = mapper.Map<List<BookDto>>(bookEntities);
        
        return bookDtos;

    }

    public async Task<BookDetailDto?> GetByIdAsync(Guid id)
    {
        var bookEntity = await bookRepository.GetByIdAsync(id);
        
        if (bookEntity == null)
            return null;
        
        // Book entitysini BookDetailDto'ya dönüştür

        #region 1. Yontem -- History bilgisini icerde doldurmak
        
        // var bookDetailDto = new BookDetailDto
        // {
        //     Id = bookEntity.Id,
        //     Title = bookEntity.Title,
        //     Author = bookEntity.Author,
        //     IsAvailable = bookEntity.IsAvailable,
        //     History = bookEntity.AssignmentHistories.Select(x=> new BookHistoryItemDto()
        //     {
        //         AssignmentHistoryId = x.Id,
        //         StudentId = x.StudentId,
        //         StudentFullName = $"{x.Student.Name} {x.Student.Surname}",
        //         AssignedDate = x.AssignedDate,
        //         ReturnedDate = x.ReturnedDate
        //     }).ToList()
        // };


        #endregion
     

        #region 2.Yontem -- History bilgisini ayrica disarda doldurmak
        
        // var bookDetailDto = new BookDetailDto
        // {
        //     Id = bookEntity.Id,
        //     Title = bookEntity.Title,
        //     Author = bookEntity.Author,
        //     IsAvailable = bookEntity.IsAvailable,
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
        
        // historyDtos listesini bookDetailDto'nun History property'sine atiyoruz
        // bookDetailDto.History = historyDtos;
        

        #endregion

        // NOT her iki yontemde de LINQ kullanarak ve ya istenirse foreach dongusu ile tek tek olusturlabilir,
        // Ben LINQ tercih ettim cunku daha kisa ve okunabilir oluyor, ama bu tamamen tercihe baglidir.

        #region 3.Yontem -- Foreach Dongusu Ile History listesini doldurma (Pek tercih edilmez)

        // var bookDetailDto = new BookDetailDto
        // {
        //     Id = bookEntity.Id,
        //     Title = bookEntity.Title,
        //     Author = bookEntity.Author,
        //     IsAvailable = bookEntity.IsAvailable,
        // };
        // // once ana bilgileri doldurduk, sonra history bilgisini dolduracagiz
        //
        // var historyDtoList = new List<BookHistoryItemDto>();
        //
        // foreach (var historyEntity in bookEntity.AssignmentHistories)
        // {
        //     var historyDtoItem = new BookHistoryItemDto()
        //     {
        //         AssignmentHistoryId = historyEntity.Id,
        //         StudentId = historyEntity.StudentId,
        //         StudentFullName = $"{historyEntity.Student.Name} {historyEntity.Student.Surname}",
        //         AssignedDate = historyEntity.AssignedDate,
        //         ReturnedDate = historyEntity.ReturnedDate
        //     };
        //     
        //     historyDtoList.Add(historyDtoItem);
        // }
        //
        // bookDetailDto.History = historyDtoList;

        #endregion

        #region 4.Yontem -- AutoMapper Ile History bilgisini doldurma (En Dogrusu)

        var bookDetailDto =  mapper.Map<BookDetailDto>(bookEntity);

        #endregion 
        
        return bookDetailDto; // olusturulan dto dondurulur
    }

    
    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
    
        var newBookEntity = mapper.Map<Book>(dto);
    
        await bookRepository.AddAsync(newBookEntity);
        
        return mapper.Map<BookDto>(newBookEntity);
    }

    public async Task<BookDto?> UpdateAsync(UpdateBookDto dto)
    {
        var bookToUpdate = await bookRepository.GetByIdAsync(dto.Id);

        if (bookToUpdate == null)
            return null;
        
        mapper.Map(dto, bookToUpdate); // dto'daki bilgileri bookToUpdate entitysinin ilgili alanlarına kopyalar

        await bookRepository.UpdateAsync(bookToUpdate);

        return mapper.Map<BookDto>(bookToUpdate);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book == null)
            return false;

        await bookRepository.DeleteAsync(book);

        return true;
    }
}