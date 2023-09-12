using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.DTOs
{
    public class CourseVideoDto
    {
        public int CourseVideoId { get; set; }
        public string CourseVideoTitle { get; set; }
        public string CourseVideoUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile CourseVideoFile { get; set; }
        public int CourseId { get; set; }
    }
}
