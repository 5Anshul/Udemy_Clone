using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class CourseVideo
    {
        public int CourseVideoId { get; set; }
        public string CourseVideoTitle { get; set; }
        public string CourseVideoUrl { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
