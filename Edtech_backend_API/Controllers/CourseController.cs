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
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(IUnitOfWork unitOfWork, IMapper mapper,IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            var courses = _unitOfWork.Courses.GetAll(includeproperties: "Category,Language,Level,CourseInstructor,CourseReviews");
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(coursesDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetCourse(int id)
        {
            var course = _unitOfWork.Courses.GetAllIncluding(c => c.CourseId == id, null, c => c.Category, c => c.Language, c => c.Level, c => c.CourseInstructor).FirstOrDefault();

            if (course == null)
            {
                return NotFound();
            }

            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }


        //[HttpPost]
        //public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto courseDto, IFormFile file, string directory)
        //{
        //    if (courseDto == null)
        //    {
        //        return BadRequest("Invalid data");
        //    }
        //    //if (file != null)
        //    //{
        //    //    string imageName = await _fileService.UploadImage(file, directory);
        //    //    return Ok(new { ImageName = imageName, Message = "Image uploaded successfully" });

        //    //}
        //    if (file != null)
        //    {
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        string fileName = Guid.NewGuid().ToString();
        //        var uploads = Path.Combine(wwwRootPath, "images", "courseimages");
        //        var extension = Path.GetExtension(file.FileName);

        //        // Delete old image if exists
        //        if (!string.IsNullOrEmpty(courseDto.CourseImageUrl))
        //        {
        //            var oldImagePath = Path.Combine(wwwRootPath, courseDto.CourseImageUrl.TrimStart(Path.DirectorySeparatorChar));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //        var newImagePath = Path.Combine(uploads, fileName + extension);
        //        using (var fileStream = new FileStream(newImagePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(fileStream);
        //        }

        //        courseDto.CourseImageUrl = Path.Combine("images", "courseimages", fileName + extension);
        //    }
        //    var course = _mapper.Map<Course>(courseDto);
        //    _unitOfWork.Courses.Add(course);
        //    _unitOfWork.Save();

        //    courseDto = _mapper.Map<CourseDto>(course);
        //    return CreatedAtAction(nameof(GetCourse), new { id = courseDto.CourseId }, courseDto);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CourseDto courseDto, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (file != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, "images", "courseimages");
                var extension = Path.GetExtension(file.FileName);

                // Delete old image if exists
                if (!string.IsNullOrEmpty(courseDto.CourseImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, courseDto.CourseImageUrl.TrimStart(Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var newImagePath = Path.Combine(uploads, fileName + extension);
                using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                courseDto.CourseImageUrl = Path.Combine("images", "courseimages", fileName + extension);
            }

            var course = _mapper.Map<Course>(courseDto);
            _unitOfWork.Courses.Add(course);
            _unitOfWork.Save();

            return Ok(courseDto);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateCourse(int id, CourseDto courseDto)
        //{
        //    if (id != courseDto.CourseId)
        //    {
        //        return BadRequest();
        //    }

        //    var course = _mapper.Map<Course>(courseDto);
        //    _unitOfWork.Courses.Update(course);
        //    _unitOfWork.Save();

        //    return Ok();
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm]  CourseDto courseDto, IFormFile file)
        {
            if (id != courseDto.CourseId || !ModelState.IsValid) return BadRequest(ModelState);
            var existingCourse = _unitOfWork.Courses.Get(id);
            if (existingCourse == null) return NotFound();

            if (courseDto.CourseImageUrl != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, "images", "courseimages");
                var extension = Path.GetExtension(file.FileName);

                // Delete old image if exists
                if (!string.IsNullOrEmpty(existingCourse.CourseImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, existingCourse.CourseImageUrl.TrimStart(Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var newImagePath = Path.Combine(uploads, fileName + extension);
                using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                {
                    //await courseDto.CourseImageUrl.CopyToAsync(fileStream);
                    await file.CopyToAsync(fileStream);
                }

                existingCourse.CourseImageUrl = Path.Combine("images", "courseimages", fileName + extension);
            }

            // Update other properties of existingCourse using courseDto
            _mapper.Map(courseDto, existingCourse);

            _unitOfWork.Courses.Update(existingCourse);
            _unitOfWork.Save();

            return Ok(courseDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _unitOfWork.Courses.Get(id);
            if (course == null)
            {
                return NotFound();
            }

            _unitOfWork.Courses.Remove(course);
            _unitOfWork.Save();

            return NoContent();
        }
    }

}
