using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public DateTime CoursePublishedDate { get; set; }
        public string CourseImageUrl { get; set; }
        public double CoursePrice { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
        public int CourseInstructorId { get; set; }

        //[DataType(DataType.Upload)]
        //public IFormFile Image { get; set; }
        public ICollection<CourseVideoDto> CourseVideos { get; set; }
        public ICollection<CourseReviewDto> CourseReviews { get; set; }
    }
}
