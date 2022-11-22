
namespace API.Dtos
{
    public class BookToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; }
        public string AuthorPseudonym  { get; set; }
    }
}