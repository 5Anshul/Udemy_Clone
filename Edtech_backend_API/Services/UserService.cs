using Edtech_backend_API.DTOs;
using Edtech_backend_API.Identity;
using Edtech_backend_API.Services.ServiceContract;
using Edtech_backend_API.StandaryDictionary;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Edtech_backend_API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly AppSettings _appSetting;
        public UserService(ApplicationUserManager applicationUserManager,
            ApplicationSignInManager applicationSignInManager, IOptions<AppSettings> appsetting)
        {
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _appSetting = appsetting.Value;
        }
        public async Task<ApplicationUser> Authenticate(LoginDto loginDto)
        {
            var result = await _applicationSignInManager.PasswordSignInAsync(loginDto.UserName, loginDto.UserPassword, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.
                  FindByNameAsync(loginDto.UserName);
                applicationUser.PasswordHash = "";

                //JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                ////if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Instructor))
                ////    applicationUser.Role = SD.Role_Instructor;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Roles_User))
                    applicationUser.Role = SD.Roles_User;


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,applicationUser.Id),
                      new Claim(ClaimTypes.Email,applicationUser.Email),
                    new Claim(ClaimTypes.Role,applicationUser.Role)
                         }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);
                applicationUser.PasswordHash = "";

                return applicationUser;
            }
            else
            {
                return null;
            }
        }
    }
}
