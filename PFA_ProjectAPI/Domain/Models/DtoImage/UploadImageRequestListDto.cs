namespace PFA_ProjectAPI.Domain.Models.DtoImage
{
    public class UploadImageRequestListDto
    {
        //public List<ImageUploadRequestDto> Files { get; set; }
        public IFormFile[] Files { get; set; }
    }
}
