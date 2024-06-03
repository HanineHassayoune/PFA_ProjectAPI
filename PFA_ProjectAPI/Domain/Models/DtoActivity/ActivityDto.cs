using System;
using PFA_ProjectAPI.Domain.Models.Domain;


namespace PFA_ProjectAPI.Domain.Models.DtoActivity
{
    public class ActivityDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Animator { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
