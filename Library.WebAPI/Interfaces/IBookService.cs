using Library.WebAPI.Common;
using Library.WebAPI.Dto;

namespace Library.WebAPI.Interfaces
{
    public interface IBookService
    {
        PagedResult<BookDto> GetListOfBooks(BookSearchDto search);

        public List<BookDto> GetListOfAllBooks();

        BookDto GetBookById(int id);

        BookDto InsertBook(BookInsertDto insertData);

        BookDto UpdateBook(int id, BookUpdateDto updateData);

        bool DeleteBook(int id);
    }
}
