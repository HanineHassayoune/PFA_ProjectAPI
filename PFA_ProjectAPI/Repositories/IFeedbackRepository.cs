using PFA_ProjectAPI.Models.Domain;

namespace PFA_ProjectAPI.Repositories
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();


        Task<Feedback?> GetByIdAsync(Guid id);


        Task<Feedback> CreateAsync(Feedback feedback);

        Task<Feedback?> DeleteAsync(Guid id);

    }
}
