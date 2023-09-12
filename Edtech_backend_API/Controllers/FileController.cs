using Edtech_backend_API.FileServices.FileServiceContract;
using Edtech_backend_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public FileController(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file, string directory)
        {
            // Upload the image 
            string imageName = await _fileService.UploadImage(file, directory);
            // Return the name of the uploaded image in a BaseResponse object
            return Ok(new{ImageName = imageName, Message="Image uploaded successfully"});
        }
        [HttpGet("Images")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            // Retrieve the image using the files service
            var imageStream = await _fileService.GetFile(imageName);

            if (imageStream == null)
            {
                return NotFound();
            }

            // Return the image as a JPEG file
            return File(imageStream, "image/jpeg");
        }
        [HttpPost("UploadVideo")]
        public async Task<IActionResult> UploadVideo(IFormFile file, string directory)
        {
            string videoName = await _fileService.UploadVideo(file, directory);
            return Ok(new {VideoName= videoName, Message= "Successfully uploaded video." });

        }
        [HttpGet("Videos")]
        public async Task<IActionResult> GetVideo(string videoName)
        {
            // Find the video by its name
            var video =  _unitOfWork.CourseVideos.GetAll(v => v.CourseVideoUrl == videoName);
            if (video == null)
            {
                return NotFound();
            }

            var videoFile = await _fileService.GetFile(videoName);
            if (videoFile == null)
            {
                return NotFound();
            }

            // Return the video file as a FileStreamResult with the appropriate MIME type and file name
            var stream = new MemoryStream(videoFile);
            return File(stream, "video/mp4");

        }
    }
}
