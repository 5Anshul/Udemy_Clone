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
    [Route("api/language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LanguageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LanguageDto>> GetLanguages()
        {
            var languages = _unitOfWork.Languages.GetAll(includeproperties: "Courses");
            var languagesDto = _mapper.Map<IEnumerable<LanguageDto>>(languages);
            return Ok(languagesDto);
        }



        [HttpGet("{id}")]
        public ActionResult<LanguageDto> GetLanguage(int id)
        {
            var language = _unitOfWork.Languages.Get(id);

            if (language == null)
            {
                return NotFound();
            }

            var languageDto = _mapper.Map<LanguageDto>(language);
            return Ok(languageDto);
        }

        [HttpPost]
        public ActionResult<LanguageDto> CreateLanguage(LanguageDto languageDto)
        {
            if (languageDto == null)
            {
                return BadRequest("Invalid data");
            }

            var language = _mapper.Map<Language>(languageDto);
            _unitOfWork.Languages.Add(language);
            _unitOfWork.Save();

            languageDto = _mapper.Map<LanguageDto>(language);
            return CreatedAtAction(nameof(GetLanguage), new { id = languageDto.LanguageId }, languageDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, LanguageDto languageDto)
        {
            if (id != languageDto.LanguageId)
            {
                return BadRequest();
            }

            var language = _mapper.Map<Language>(languageDto);
            _unitOfWork.Languages.Update(language);
            _unitOfWork.Save();

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            var language = _unitOfWork.Languages.Get(id);
            if (language == null)
            {
                return NotFound();
            }

            _unitOfWork.Languages.Remove(language);
            _unitOfWork.Save();

            return NoContent();
        }
    }

}
