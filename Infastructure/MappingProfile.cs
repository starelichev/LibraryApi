using AutoMapper;
using Library.Domain.Application.Models;
using Library.Models.Application.ViewModels;

namespace LibraryApiApplication.Infastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorViewModel>();
        CreateMap<Book, BookViewModel>();
        CreateMap<Employee, EmployeeViewModel>();
        CreateMap<Reader, ReaderViewModel>();
        CreateMap<History, HistoryViewModel>();
    }
}