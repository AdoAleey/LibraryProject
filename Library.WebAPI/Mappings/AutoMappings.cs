using AutoMapper;
using Library.WebAPI.Dto;
using Library.WebAPI.Entities;

namespace Library.WebAPI.Mappings
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<Book, BookDto>();
          //      .ForMember(x => x.PublishedYear, y => y.MapFrom(a => a.PublishedYear));

            CreateMap<BookInsertDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
    }
}
