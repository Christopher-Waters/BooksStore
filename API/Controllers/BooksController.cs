using System.Net;
using System.Security.Claims;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        public BooksController(DataContext context, IMapper mapper, UserManager<Author> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<BookToReturnDto>>> GetBooks([FromQuery] string title)
        {

            var books = await _context.Books
            .Where(c => EF.Functions.Like(c.Title.ToLower(), $"%{title}%"))
            .Include(c => c.Author)
            .ToListAsync();

            return Ok(_mapper.Map<List<Book>, List<BookToReturnDto>>(books));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookToReturnDto>> UnpublishBookByID(int id)
        {

            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
           .Value;

            var user = await _userManager.FindByEmailAsync(email);

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("AdminAuthor")) return Unauthorized();

            var book = await _context.Books.Include(c => c.Author).FirstOrDefaultAsync(x => x.Id == id);

            var message = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Authors can only delete their own books" };


            if (user.Id != book.AuthorId) return Unauthorized(message);

            _context.Entry(book).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PublishBook([FromBody] CreateBookDto book)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
           .Value;

            var user = await _userManager.FindByEmailAsync(email);
            try
            {
                var bookPost = new CreateBookDto
                {
                    Title = book.Title,
                    Price = book.Price,
                    Description = book.Description,
                    CoverImageUrl = book.CoverImageUrl,
                    AuthorId = user.Id

                };

                Book bookToInsert = _mapper.Map<Book>(bookPost);
                _context.Add(bookToInsert);
                await _context.SaveChangesAsync();

                return Ok(bookToInsert);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [Authorize]
        [HttpPut]
         public async Task<ActionResult> UpdateBook([FromBody] UpdateBookDto book)
         {
            if (!ModelState.IsValid)
                return BadRequest();

           var books = _context.Books.Find(book.Id);

           if(books == null)  return NotFound();

           books.Price = book.Price;
           books.Title = book.Title;
           books.Description = book.Description;
           books.CoverImageUrl = book.CoverImageUrl ;

           //Book BookToUpdate = _mapper.Map<Book>(books) ;
           _context.Entry(books).State = EntityState.Modified;
           await _context.SaveChangesAsync();
           return Ok(book);

         }

    }
}