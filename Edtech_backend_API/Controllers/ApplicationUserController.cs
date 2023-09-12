using Edtech_backend_API.DTOs;
using Edtech_backend_API.Identity;
using Edtech_backend_API.StandaryDictionary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Controllers
{
    [Route("api/applicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController( UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var user = new ApplicationUser { UserFullName = userRegisterDto.UserFullName,UserName=userRegisterDto.UserName, UserAddress =userRegisterDto.UserAddress};
            var result = await _userManager.CreateAsync(user, userRegisterDto.UserPassword);
            if (result.Succeeded)
            {
                // Assign the role to the Admin
                await _userManager.AddToRoleAsync(user, SD.Roles_User);

                // Return the response
                return Ok();
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.UserPassword, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded) return Ok(); //// User was successfully logged in
            return BadRequest("Invalid login attempt.");
        }
    }
}
