using System;
using System.Threading.Tasks;
using PFA_ProjectAPI.Models.Domain;

namespace PFA_ProjectAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<Image> GetImageByEventIdAsync(Guid eventId);
    }
}
