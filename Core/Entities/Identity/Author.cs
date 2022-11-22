using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class Author : IdentityUser<int>
    {
        public string AuthorPseudonym { get; set; }

        public ICollection<AppUserRole> UserRoles {get; set;}

        public ICollection<Book> Books { get; set; }
    }
}