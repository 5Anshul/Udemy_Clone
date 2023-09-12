using Edtech_backend_API.Data;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository
{
    public class CourseVideoRepository : Repository<CourseVideo>, ICourseVideoRepository
    {
        public CourseVideoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
