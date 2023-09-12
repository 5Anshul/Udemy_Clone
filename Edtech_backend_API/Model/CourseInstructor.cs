using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class CourseInstructor
    {
        public int CourseInstructorId { get; set; }
        public string CourseInstructorName { get; set; }
        public string CourseInstructorAbout { get; set; }
        public string CourseInstructorReviews { get; set; }
        public int CourseInstructorRating { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Course>Courses { get; set; }

    }
}
