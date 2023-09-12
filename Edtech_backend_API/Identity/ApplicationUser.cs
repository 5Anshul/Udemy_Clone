using Edtech_backend_API.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string UserFullName { get; set; }

        public string UserAddress { get; set; }
        public string UserProfilePictureUrl { get; set; } = "default.jpg"!;
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public ICollection<CourseReview> CourseReviews { get; set; }
        public ICollection<Course> Courses { get; set; } = null!;
    }
}
