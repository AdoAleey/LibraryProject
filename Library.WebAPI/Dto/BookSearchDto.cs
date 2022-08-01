namespace Library.WebAPI.Dto
{
    public class BookSearchDto
    {        
        /// <summary>
        /// Broj rezultata po stranici
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Broj stranice koja se vraća
        /// </summary>
        public int Page { get; set; }

        public string? Title { get; set; }
    }
}
