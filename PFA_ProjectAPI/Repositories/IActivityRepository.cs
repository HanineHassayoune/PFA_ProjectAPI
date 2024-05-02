using PFA_ProjectAPI.Models.ActivityModels;

namespace PFA_ProjectAPI.Repositories
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetAllAsync();
    }
}
