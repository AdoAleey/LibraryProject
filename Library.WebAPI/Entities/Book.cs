namespace Library.WebAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public string AuthorName { get; set; }
        public int? NumberOfPages { get; set; }
        public string? Language { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
    }
}
