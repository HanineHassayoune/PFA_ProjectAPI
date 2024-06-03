using PFA_ProjectAPI.Domain.Models.Domain;

namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();


        Task<Feedback?> GetByIdAsync(Guid id);


        Task<Feedback> CreateAsync(Feedback feedback);

        Task<Feedback?> DeleteAsync(Guid id);

    }
}
