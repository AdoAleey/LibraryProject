namespace Library.WebAPI.Common
{
    //PagedResult<GenericClass>
    //GenericClass - bilo koja klasa T
    public class PagedResult<T>
    {
        public long TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}


//PagedResult<Book>
//PagedResult<Author>
//PagedResult<GenericClass?>