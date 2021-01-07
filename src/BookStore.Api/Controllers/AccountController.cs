﻿using System.Threading.Tasks;
using BookStore.Api.Dtos;
using BookStore.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BookStore.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponseType());

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,
                false);

            if (!result.Succeeded) return Unauthorized(new ApiResponseType());

            return new UserDto
            {
                Email = user.Email,
                Token = "this is a token",
                DisplayName = user.DisplayName
            };
        }
    }
}
