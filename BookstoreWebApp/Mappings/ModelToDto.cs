using AutoMapper;
using BookstoreWebApp.Dtos;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Mappings
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Book, BookDto>();
        }
    }
}