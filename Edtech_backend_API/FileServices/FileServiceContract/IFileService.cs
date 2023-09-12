using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.FileServices.FileServiceContract
{
    public interface IFileService
    {
        Task<byte[]> GetFile(string fileName);
        Task<string> UploadImage(IFormFile file, string directory);
        Task<string> UploadVideo(IFormFile file, string directory);
        Task DeleteAsync(string path);
    }
}
