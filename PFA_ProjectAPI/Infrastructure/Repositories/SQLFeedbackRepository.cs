using Microsoft.EntityFrameworkCore;
using PFA_ProjectAPI.Domain.Models.Domain;
using PFA_ProjectAPI.Infrastructure.Data;

namespace PFA_ProjectAPI.Infrastructure.Repositories
{
    public class SQLFeedbackRepository : IFeedbackRepository
    {
        private readonly TBDbContext dbContext;
        public SQLFeedbackRepository(TBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Feedback> CreateAsync(Feedback feedback)
        {
            await dbContext.Feedbacks.AddAsync(feedback);
            await dbContext.SaveChangesAsync();
            return feedback;

        }

        public async Task<Feedback?> DeleteAsync(Guid id)
        {
            // Find the feedback in the DB
            var existingFeedback = await dbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingFeedback == null)
            {
                return null;
            }
            dbContext.Feedbacks.Remove(existingFeedback);
            await dbContext.SaveChangesAsync();
            return existingFeedback;

        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await dbContext.Feedbacks.ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(Guid id)
        {
            return await dbContext.Feedbacks.FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
