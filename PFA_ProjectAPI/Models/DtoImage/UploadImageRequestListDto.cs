namespace PFA_ProjectAPI.Models.DtoImage
{
    public class UploadImageRequestListDto
    {
        //public List<ImageUploadRequestDto> Files { get; set; }
        public IFormFile[] Files { get; set; }
    }
}
