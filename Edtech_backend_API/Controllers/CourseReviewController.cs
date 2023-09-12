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
    [Route("api/courseReview")]
    [ApiController]
    public class CourseReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseReviewDto>> GetCourseReviews()
        {
            var reviews = _unitOfWork.CourseReviews.GetAll(includeproperties: "Course");
            var reviewsDto = _mapper.Map<IEnumerable<CourseReviewDto>>(reviews);
            return Ok(reviewsDto);
        }
        [HttpGet("{id}")]
        public ActionResult<CourseReviewDto> GetCourseReview(int id)
        {
            var review = _unitOfWork.CourseReviews.GetAllIncluding(c => c.CourseId == id).FirstOrDefault();

            if (review == null)
            {
                return NotFound();
            }

            var reviewDto = _mapper.Map<CourseReviewDto>(review);
            return Ok(reviewDto);
        }
        [HttpPost]
        public ActionResult<CourseReviewDto> CreateCourseReview(CourseReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest("Invalid data");
            }

            var review = _mapper.Map<CourseReview>(reviewDto);
            _unitOfWork.CourseReviews.Add(review);
            _unitOfWork.Save();

            reviewDto = _mapper.Map<CourseReviewDto>(review);
            return CreatedAtAction(nameof(GetCourseReview), new { id = reviewDto.CourseReviewId }, reviewDto);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCourseReview(int id, CourseReviewDto reviewDto)
        {
            if (id != reviewDto.CourseReviewId)
            {
                return BadRequest();
            }

            var review = _mapper.Map<CourseReview>(reviewDto);
            _unitOfWork.CourseReviews.Update(review);
            _unitOfWork.Save();

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCourseReview(int id)
        {
            var review = _unitOfWork.CourseReviews.Get(id);
            if(review==null)
            {
                return NotFound();
            }
            _unitOfWork.CourseReviews.Remove(review);
            _unitOfWork.Save();

            return NoContent();
        }
    } 
}
