using Edtech_backend_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class CourseInstructorDto
    {
        public int CourseInstructorId { get; set; }
        public string CourseInstructorName { get; set; }
        public string CourseInstructorAbout { get; set; }
        public string CourseInstructorReviews { get; set; }
        public string CourseInstructorComment { get; set; }
        public int CourseInstructorRating { get; set; }
        public string UserId { get; set; }
        //public ApplicationUserDto ApplicationUser { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}

