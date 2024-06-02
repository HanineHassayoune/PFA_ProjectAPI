using Microsoft.Extensions.Hosting;

namespace PFA_ProjectAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
