using AutoMapper;
using Library.WebAPI.Common;
using Library.WebAPI.Dto;
using Library.WebAPI.Entities;
using Library.WebAPI.Exceptions;
using Library.WebAPI.Infrastructure.Database;
using Library.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Services
{
    public class BookService : IBookService
    {
        protected readonly LibraryDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BookService> _logger;

        public BookService(LibraryDbContext context, IMapper mapper, ILogger<BookService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public bool DeleteBook(int id)
        {
            var dbBook = _context.Books.FirstOrDefault(x=>x.Id == id);
            if (dbBook == null)
            {
                throw new NoBookException("Trazena knjiga ne postoji u bazi.");
            }

            _context.Books.Remove(dbBook);
            
            //var deleted = _context.Books.Remove(dbBook);
            // _logger.LogInformation($"Obrisana je {deleted} knjiga");

            //vrati True ako je brisanje OK, SaveChange() vraca broj(integer) zahvacenih rekorda 
            return _context.SaveChanges() > 0;
        }

        public BookDto GetBookById(int id)
        {
            var dbBook = _context.Books.FirstOrDefault(x => x.Id == id);
            //var dbBook = _context.Books.Where(x=>x.Id==id).FirstOrDefault();    

            if (dbBook == null)
            {
                throw new NoBookException("Trazena knjiga ne postoji u bazi.");
            }

            return _mapper.Map<BookDto>(dbBook);
        }

        public List<BookDto> GetListOfAllBooks()
        {
            var databaseEntityList = _context.Books.ToList();
            //var newListBooks = new List<BookDto>();

            //foreach (var book in databaseEntityList)
            //{
            //    newListBooks.Add(new BookDto() { Id = book.Id, Title = book.Title, AuthorName = book.AuthorName, PublishedYear = book.PublishedYear });
            //}

            //return newListBooks;

            return _mapper.Map<List<BookDto>>(databaseEntityList);
        }
        public PagedResult<BookDto> GetListOfBooks(BookSearchDto search)
        {
            var query = _context.Books.AsQueryable();
            
            if (!string.IsNullOrEmpty(search.Title))
            {
                query = query.Where(x=>x.Title.ToLower() == search.Title.ToLower());
            }

            PagedResult<BookDto> result = new();
            result.TotalCount = query.LongCount();

            query = query.Skip(search.Page * search.PageSize)
                .Take(search.PageSize);

            List<Book> res = query.ToList();
            result.Data = _mapper.Map<List<BookDto>>(res);
            return result;
        }

        public BookDto InsertBook(BookInsertDto insertData)
        {
            var dbBook = _mapper.Map<Book>(insertData);
            _context.Books.Add(dbBook);
            _context.SaveChanges();
            return _mapper.Map<BookDto>(dbBook);
        }

        public BookDto UpdateBook(int id, BookUpdateDto updateData)
        {
            _logger.LogInformation($"Mijenja se knjiga sa id {id}");

            var dbBook = _context.Books.FirstOrDefault(x => x.Id == id);

            if (dbBook == null)
            {
                //throw new Exception("Nema knjige!");
                throw new NoBookException("Trazena knjiga ne postoji u bazi.");
            }

            _mapper.Map(updateData, dbBook);
            _context.SaveChanges();

            return _mapper.Map<BookDto>(dbBook);
        }
    }
}
