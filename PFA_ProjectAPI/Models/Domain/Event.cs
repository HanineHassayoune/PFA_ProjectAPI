using PFA_ProjectAPI.Models.Enums;
using System.Reflection.Metadata;

namespace PFA_ProjectAPI.Models.Domain
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

        // Collection navigation property
        public ICollection<Activity> Activities { get; set; }

        // Navigation property for one-to-one relationship
        public Image? Image { get; set; }

    }
}
