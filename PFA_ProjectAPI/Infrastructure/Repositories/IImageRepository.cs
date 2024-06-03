using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PFA_ProjectAPI.Domain.Models.Domain;


namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<Image> GetImageByEventIdAsync(Guid eventId);

        Task<IEnumerable<Image>> GetImagesByActivityIdAsync(Guid activityId);


    }
}
