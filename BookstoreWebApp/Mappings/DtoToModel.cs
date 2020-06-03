using AutoMapper;
using BookstoreWebApp.Dtos;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Mappings
{
    public class DtoToModel : Profile
    {
        public DtoToModel()
        {
            CreateMap<BookDto, Book>();
        }
    }
}