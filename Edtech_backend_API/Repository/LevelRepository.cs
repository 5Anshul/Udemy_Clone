using Edtech_backend_API.Data;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository
{
    public class LevelRepository:Repository<Level>, ILevelRepository
    {
        public LevelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
