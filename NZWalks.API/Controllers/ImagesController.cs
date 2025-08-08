using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileName = imageUploadRequestDto.FileName,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length

                };
                image = await imageRepository.Upload(image);

                return Ok(image);
            }

            return BadRequest(ModelState);
        }
        private void ValidateUpload(ImageUploadRequestDto imageUploadRequestDto) 
        {
            string[] allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Extension not supported!");
            }

            if (imageUploadRequestDto.File.Length > 10428670) 
            {
                ModelState.AddModelError("file", "Upload image size is above the limit");
            }


        }
    }
}
