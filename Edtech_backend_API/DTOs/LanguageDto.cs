using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class LanguageDto
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}
