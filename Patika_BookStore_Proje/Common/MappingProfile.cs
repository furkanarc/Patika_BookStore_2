using AutoMapper;
using Patika_BookStore_Proje.BookOperations.CreateBook;
using Patika_BookStore_Proje.BookOperations.GetBookById;
using Patika_BookStore_Proje.BookOperations.GetBooks;

namespace Patika_BookStore_Proje.Common{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }

}
