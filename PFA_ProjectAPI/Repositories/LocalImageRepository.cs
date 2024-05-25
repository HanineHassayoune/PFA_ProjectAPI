using API.Data;
using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Models.Domain;

namespace PFA_ProjectAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TBDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment ,
            IHttpContextAccessor httpContextAccessor , TBDbContext dbContext)
        {
            this.webHostEnvironment= webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> GetImageByEventIdAsync(Guid eventId)
        {
            return await dbContext.Images.FirstOrDefaultAsync(i => i.EventId == eventId);
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg
            //the url path that wiill be upload to table
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //add Image to  the table 
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
