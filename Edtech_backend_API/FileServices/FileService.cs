﻿using Edtech_backend_API.FileServices.FileServiceContract;
using Edtech_backend_API.Repository;
using Edtech_backend_API.StandaryDictionary.ApiExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Edtech_backend_API.FileServices
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }
        private string PathRoot(string nameFile)
        {
            return Path.Combine(Environment.CurrentDirectory, "wwwroot", nameFile);
        }
        public async Task DeleteAsync(string path)
        {
            var fullPath = PathRoot(path);
            if (System.IO.File.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => System.IO.File.Delete(fullPath));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while deleting the file: {ex.Message}");
                }
            }
            else if (Directory.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => Directory.Delete(fullPath, true));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while deleting the Directory: {ex.Message}");
                }
            }
            else
            {
                _logger.LogError($"Failed to delete {path}");
            }
        }


        public async Task<byte[]> GetFile(string fileName)
        {
            // Gets the file path.
            var path = PathRoot(fileName);

            // Reads the file into a byte array and returns it.
            return await System.IO.File.ReadAllBytesAsync(path);
        }



        public async Task<string> UploadImage(IFormFile file, string directory)
        {
            // Defines the allowed file extensions for image files.
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            // Gets the extension of the uploaded file.
            string fileExtension = Path.GetExtension(file.FileName);

            // Checks if the file extension is allowed for images.
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ApiException(
                    HttpStatusCode.BadRequest,
                    $"Invalid file type. Only JPG, PNG , WEBP and GIF files are allowed."
                );
            }
            // Uploads the file and returns the file name.
            return await UploadFile(file, directory);
        }

        public async Task<string> UploadVideo(IFormFile file, string directory)
        {
            // Defines the allowed file extensions for video files.
            string[] allowedExtensions = { ".mp4", ".avi", ".mov" };

            // Gets the extension of the uploaded file.
            string fileExtension = Path.GetExtension(file.FileName);

            // Checks if the file extension is allowed for videos.
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ApiException(
                    HttpStatusCode.BadRequest,
                    $"Invalid file type. Only MP4, AVI and MOV files are allowed."
                );
            }

            // Uploads the file and returns the file name.
            return await UploadFile(file, directory);
        }

        private async Task<string> UploadFile(IFormFile file, string directory)
        {
            try
            {

                var pathDirectory = PathRoot(directory);

                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }

                var path = PathRoot(Path.Combine(pathDirectory, file.FileName));

                // Creates a file stream and copies the uploaded file to it.
                using FileStream fileStream = new(path, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return directory + "/" + file.FileName;
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, "Upload failed: " + ex.Message);
            }
        }
    }
}

