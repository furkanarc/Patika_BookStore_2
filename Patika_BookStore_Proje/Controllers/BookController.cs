using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Patika_BookStore_Proje.BookOperations.CreateBook;
using Patika_BookStore_Proje.BookOperations.DeleteBook;
using Patika_BookStore_Proje.BookOperations.GetBookById;
using Patika_BookStore_Proje.BookOperations.GetBooks;
using Patika_BookStore_Proje.BookOperations.UpdateBook;
using Patika_BookStore_Proje.DBOperations;

namespace Patika_BookStore_Proje.AddControllers{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase{

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookViewModel result;
            try
            {
                GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
                query.BookId = id;
                GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }

        // GetById FromQuery

        // [HttpGet]
        // public Book GetById([FromQuery] int id){
        //     var book = BookList.Where(x => x.Id == id).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("özellik" + item.PropertyName + "Error Message" + item.ErrorMessage);
                
                    
                
                
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook){

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);    
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){

            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            return Ok();
        }

    }
}