using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class CourseReviewDto
    {
        public int CourseReviewId { get; set; }
        public string CourseComment { get; set; }
        public DateTime CourseReviewCreatedAt { get; set; }
        public int CourseRating { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}
