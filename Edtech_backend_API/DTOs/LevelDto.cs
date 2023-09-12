using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class LevelDto
    {
        public int LevelId { get; set; }

        public string LevelName { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}
