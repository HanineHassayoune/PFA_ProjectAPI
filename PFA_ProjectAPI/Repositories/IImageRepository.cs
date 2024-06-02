using System;
using System.Threading.Tasks;
using PFA_ProjectAPI.Models.Domain;
using System.Collections.Generic;


namespace PFA_ProjectAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<Image> GetImageByEventIdAsync(Guid eventId);

        Task<IEnumerable<Image>> GetImagesByActivityIdAsync(Guid activityId);


    }
}
