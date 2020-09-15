using API.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ImagesController : BaseApiController
    {
        private readonly IWebHostEnvironment _env;

        public ImagesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> UploadGameImage([FromForm]IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest();

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".jpg", ".png", ".bmp" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a valid image");

            string filePath = Path.Combine(_env.WebRootPath, "images/games/", fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fs);
            }

            return Ok($"/images/{fileName}");
        }
    }
}
