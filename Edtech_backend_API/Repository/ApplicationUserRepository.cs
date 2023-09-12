using Edtech_backend_API.Data;
using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository.IRepository
{
    public class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRespository
    {
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
