using Edtech_backend_API.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Edtech_backend_API.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}