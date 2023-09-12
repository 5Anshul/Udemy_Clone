using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public DateTime CoursePublishedDate { get; set; }
        public int EstimatedCourseTime { get; set; }
        public string CourseImageUrl { get; set; }
        public double CoursePrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int CourseInstructorId { get; set; }
        public CourseInstructor CourseInstructor { get; set; }
        public  string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public ICollection<CourseVideo> CourseVideos { get; set; }
        public ICollection<CourseReview>CourseReviews { get; set; }
    }
}
