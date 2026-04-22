using AutoMapper;
using WebApplication2.DTOs;
using WebApplication2.Entities;
using WebApplication2.Models.DTOs;
using WebApplication2.Models.Entities;
using WebApplication2.Models.VMs;
using WebApplication2.VMs;

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

        CreateMap<Student, StudentDto>().ReverseMap();

        CreateMap<AssignmentHistory, StudentHistoryItemDto>()
            .ForMember(dest => dest.AssignmentHistoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.BookAuthor, opt => opt.MapFrom(src => src.Book.Author))
            .ReverseMap();

        CreateMap<Student, StudentDetailDto>()
            .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.AssignmentHistories))
            .ReverseMap();

        CreateMap<CreateStudentDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ReverseMap();

        CreateMap<UpdateStudentDto, Student>();

        #endregion

        #region Student-VM-Mapping

        CreateMap<StudentDto, StudentResponseVm>().ReverseMap();

        CreateMap<StudentHistoryItemDto, StudentHistoryItemVm>().ReverseMap();

        CreateMap<StudentDetailDto, StudentDetailResponseVm>().ReverseMap();

        CreateMap<CreateStudentRequestVm, CreateStudentDto>().ReverseMap();

        CreateMap<UpdateStudentRequestVm, UpdateStudentDto>().ReverseMap();

        #endregion

        #region Library-VM-Mapping

        CreateMap<BorrowBookRequestVm, BorrowBookDto>().ReverseMap();

        CreateMap<LoanResultDto, LoanResultVm>().ReverseMap();

        CreateMap<ReturnBookRequestVm, ReturnBookDto>().ReverseMap();

        CreateMap<AssignmentHistory, LoanResultDto>()
            .ForMember(dest => dest.AssignmentHistoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ReturnedDate == null))
            .ReverseMap();

        #endregion
    }
}