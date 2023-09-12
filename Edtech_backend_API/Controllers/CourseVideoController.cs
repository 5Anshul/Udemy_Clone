using AutoMapper;
using Edtech_backend_API.DTOs;
using Edtech_backend_API.FileServices.FileServiceContract;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Controllers
{
    [Route("api/courseVideo")]
    [ApiController]
    public class CourseVideoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CourseVideoController(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseVideoDto>> GetCourseVideos()
        {
            var courseVideos = _unitOfWork.CourseVideos.GetAll(includeproperties: "Course");
            var courseVideoDto = _mapper.Map<IEnumerable<CourseVideoDto>>(courseVideos);
            return Ok(courseVideoDto);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CourseVideoDto>> GetCourseVideo(int id)
        {
            var Video = _unitOfWork.CourseVideos.GetAllIncluding(c => c.CourseId == id).FirstOrDefault();
            if (Video == null)
            {
                return BadRequest("Not Found");
            }
            var courseVideo = _mapper.Map<CourseVideoDto>(Video);
            return Ok(courseVideo);
        }
        [HttpPost]
       public async Task< ActionResult<IEnumerable<CourseVideoDto>>> CreateCourseVideo([FromForm] CourseVideoDto courseVideoDto)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (courseVideoDto.CourseVideoUrl != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Videos\CourseVideos");
                    var extension = Path.GetExtension(courseVideoDto.CourseVideoFile.FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                       await courseVideoDto.CourseVideoFile.CopyToAsync(fileStream);
                    }
                    courseVideoDto.CourseVideoUrl = @"\Videos\CourseVideos\" + fileName + extension;

                    var courseVideo = new CourseVideo
                    {
                        CourseVideoTitle = courseVideoDto.CourseVideoTitle,
                        CourseVideoUrl = courseVideoDto.CourseVideoUrl,
                        CourseId = courseVideoDto.CourseId
                    };
                    string videoName = await _fileService.UploadVideo(courseVideoDto.CourseVideoFile, @"Videos\CourseVideos");
                    var courseVideos = _mapper.Map<CourseVideo>(courseVideoDto);
                    courseVideo.CourseVideoUrl = videoName;
                    _unitOfWork.CourseVideos.Add(courseVideo);
                    _unitOfWork.Save();

                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
