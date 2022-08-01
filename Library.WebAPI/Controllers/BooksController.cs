using Library.WebAPI.Common;
using Library.WebAPI.Dto;
using Library.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public BookDto GetBookById(int id)
        {
            return _bookService.GetBookById(id);
        }

        [HttpGet("")]
        public PagedResult<BookDto> GetBooks([FromQuery] BookSearchDto search)
        {
            //doci ce podatak iz queryStringa [FromQuery], queryStringa: 'Books?PageSize=0&Page=1&Title=King%20Kong'
            _logger.LogInformation("Ovo je log: GetBooks kontrolera");
            return _bookService.GetListOfBooks(search);
        }

        [HttpGet("all")]
        public List<BookDto> GetAllBooks()
        {
            _logger.LogInformation("###Ovo je log: GetListOfAllBooks kontrolera!###");
            return _bookService.GetListOfAllBooks();
        }

        [HttpPost]
        public BookDto CreateBook(BookInsertDto insertBookRequest)
        {
            return _bookService.InsertBook(insertBookRequest);
        }

        [HttpPut("{id}")]
        public BookDto UpdateBook(int id, BookUpdateDto updateRequest)
        {
            return _bookService.UpdateBook(id, updateRequest);
        }

        [HttpDelete]
        public bool DeleteBook(int id)
        {
            //vrati True ako je brisanje OK
            return _bookService.DeleteBook(id);
        }
    }
}
