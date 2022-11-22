
namespace API.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
    }
}