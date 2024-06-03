using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace PFA_ProjectAPI.Domain.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Creator { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }

        // Collection navigation properties
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
