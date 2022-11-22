
using System.Net;
using API.Dtos;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<Author> _userManager;
        private readonly SignInManager<Author> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<Author> userManager, SignInManager<Author> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            var message = new HttpResponseMessage(HttpStatusCode.Unauthorized) {ReasonPhrase="User-Password Conbination not found"};

            if (user == null) return Unauthorized(message);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized(message);

            return new UserDto{
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                AuthorPseudonym = user.AuthorPseudonym
            };


        }

    }
}