using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class CourseReview
    {
        public int CourseReviewId { get; set; }
        public string CourseComment { get; set; }
        public DateTime CourseReviewCreatedAt { get; set; }
        public int CourseRating { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
