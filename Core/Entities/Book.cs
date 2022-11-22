using Core.Entities.Identity;

namespace Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}