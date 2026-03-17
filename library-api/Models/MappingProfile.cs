using AutoMapper;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Models.VMs;

namespace WebApplication2.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        #region Book-DTO-Mapping
        
        CreateMap<Book, BookDto>().ReverseMap();


        CreateMap<AssignmentHistory, BookHistoryItemDto>()
            .ForMember(dest => dest.AssignmentHistoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.Name + " " + src.Student.Surname))
            .ReverseMap();
        
        CreateMap<Book, BookDetailDto>()
            .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.AssignmentHistories))
            .ReverseMap();

        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ReverseMap();

        CreateMap<UpdateBookDto, Book>();
        
        #endregion

        #region Book-VM-Mapping

        CreateMap<BookDto, BookResponseVm>().ReverseMap();

        CreateMap<BookHistoryItemDto, BookHistoryItemVm>().ReverseMap();

        CreateMap<BookDetailDto, BookDetailResponseVm>().ReverseMap();

        CreateMap<CreateBookRequestVm, CreateBookDto>().ReverseMap();

        CreateMap<UpdateBookRequestVm, UpdateBookDto>().ReverseMap();

        #endregion


        #region Student-DTO-Mapping

        // TODO: 

        #endregion

        #region Student-VM-Mapping

        // TODO: 

        #endregion
    }
}