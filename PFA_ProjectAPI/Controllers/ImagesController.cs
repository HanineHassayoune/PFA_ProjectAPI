using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Domain.Models.DtoImage;
using PFA_ProjectAPI.Infrastructure.Repositories;

namespace PFA_ProjectAPI.Controllers
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
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {

                //convert dto to domain model


                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileName = request.FileName
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtension.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
        }



        // GET: /api/Images/{eventId}
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetImage(Guid eventId)
        {
            var image = await imageRepository.GetImageByEventIdAsync(eventId);
            if (image == null)
            {
                return NotFound();
            }

            // Return the image file or its details as needed
            return Ok(image);
        }


        // GET: /api/Images/GetImagesByActivity/{activityId}
        [HttpGet]
        [Route("GetImagesByActivity/{activityId}")]
        public async Task<IActionResult> GetImagesByActivity(Guid activityId)
        {
            var images = await imageRepository.GetImagesByActivityIdAsync(activityId);
            if (images == null || !images.Any())
            {
                return NotFound();
            }
            return Ok(images);
        }
    }
}

