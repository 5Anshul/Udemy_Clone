using Edtech_backend_API.DTOs;
using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Services.ServiceContract
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(LoginDto loginDto);
    }
}
