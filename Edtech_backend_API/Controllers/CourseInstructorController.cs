using AutoMapper;
using Edtech_backend_API.DTOs;
using Edtech_backend_API.Model;
using Edtech_backend_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Controllers
{
    [Route("api/courseinstructor")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseInstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseInstructorDto>> GetCourseInstructors()
        {
            var instructors = _unitOfWork.CourseInstructors.GetAll(includeproperties: "Courses");
            var instructorsDto = _mapper.Map<IEnumerable<CourseInstructorDto>>(instructors);
            return Ok(instructorsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructorDto> GetCourseInstructor(int id)
        {
            var instructor = _unitOfWork.CourseInstructors.Get(id);

            if (instructor == null)
            {
                return NotFound();
            }

            var instructorDto = _mapper.Map<CourseInstructorDto>(instructor);
            return Ok(instructorDto);
        }

        [HttpPost]
        public ActionResult<CourseInstructorDto> CreateCourseInstructor(CourseInstructorDto instructorDto)
        {
            if (instructorDto == null)
            {
                return BadRequest("Invalid data");
            }

            var instructor = _mapper.Map<CourseInstructor>(instructorDto);
            _unitOfWork.CourseInstructors.Add(instructor);
            _unitOfWork.Save();

            instructorDto = _mapper.Map<CourseInstructorDto>(instructor);
            return CreatedAtAction(nameof(GetCourseInstructor), new { id = instructorDto.CourseInstructorId }, instructorDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourseInstructor(int id, CourseInstructorDto courseInstructorDto)
        {
            if (id != courseInstructorDto.CourseInstructorId)
            {
                return BadRequest();
            }

            var instructor = _mapper.Map<CourseInstructor>(courseInstructorDto);
            _unitOfWork.CourseInstructors.Update(instructor);
            _unitOfWork.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourseInstructor(int id)
        {
            var instructor = _unitOfWork.CourseInstructors.Get(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _unitOfWork.CourseInstructors.Remove(instructor);
            _unitOfWork.Save();

            return NoContent();
        }
    }

}
