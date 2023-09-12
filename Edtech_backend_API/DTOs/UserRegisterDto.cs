using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public string UserPassword { get; set; }
    }
}
