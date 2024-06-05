using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace PFA_ProjectAPI.Controllers
{
    public class EventUserController : Controller
    {
        private readonly TBDbContext dbContext;

        public EventUserController(TBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Post: api/EventUser/AddEventUser (eventId et userId)
        [HttpPost("participations")]
        public async Task<IActionResult> AddEvenUser(Guid userId, Guid eventId)
        {
            // Get the user by id
            var user = await dbContext.Users.Include(u=>u.Events).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Get the event by id
            var eventEntity = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            if (eventEntity == null)
            {
                return NotFound("Event not found.");
            }

            // Add the event to the user's list of events
            user.Events.Add(eventEntity);

            // Update the user in the database
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            return Ok("Event added to user's list successfully.");
        }


       

    }
}
