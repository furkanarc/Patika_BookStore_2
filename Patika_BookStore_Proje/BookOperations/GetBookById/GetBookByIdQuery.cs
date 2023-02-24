using System;
using System.Linq;
using AutoMapper;
using Patika_BookStore_Proje.Common;
using Patika_BookStore_Proje.DBOperations;

namespace Patika_BookStore_Proje.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        public int BookId {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookViewModel Handle(){
            var book =  _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }

            BookViewModel vm = _mapper.Map<BookViewModel>(book);

            vm.Title = book.Title;
            vm.PageCount= book.PageCount;
            vm.PublishDate= book.PublishDate.Date.ToString("dd/MM/yyy");
            vm.Genre=((GenreEnum)book.GenreId).ToString();

            return vm;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}