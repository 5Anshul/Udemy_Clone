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
    [Route("api/level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LevelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LevelDto>> GetLevels()
        {
            var levels = _unitOfWork.Levels.GetAll(includeproperties: "Courses");
            var levelsDto = _mapper.Map<IEnumerable<LevelDto>>(levels);
            return Ok(levelsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<LevelDto> GetLevel(int id)
        {
            var level = _unitOfWork.Levels.Get(id);

            if (level == null)
            {
                return NotFound();
            }

            var levelDto = _mapper.Map<LevelDto>(level);
            return Ok(levelDto);
        }

        [HttpPost]
        public ActionResult<LevelDto> CreateLevel(LevelDto levelDto)
        {
            if (levelDto == null)
            {
                return BadRequest("Invalid data");
            }

            var level = _mapper.Map<Level>(levelDto);
            _unitOfWork.Levels.Add(level);
            _unitOfWork.Save();

            levelDto = _mapper.Map<LevelDto>(level);
            return CreatedAtAction(nameof(GetLevel), new { id = levelDto.LevelId }, levelDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLevel(int id, LevelDto levelDto)
        {
            if (id != levelDto.LevelId)
            {
                return BadRequest();
            }

            var level = _mapper.Map<Level>(levelDto);
            _unitOfWork.Levels.Update(level);
            _unitOfWork.Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLevel(int id)
        {
            var level = _unitOfWork.Levels.Get(id);
            if (level == null)
            {
                return NotFound();
            }

            _unitOfWork.Levels.Remove(level);
            _unitOfWork.Save();

            return NoContent();
        }
    }

}
