using PFA_ProjectAPI.Migrations;
using System.Reflection.Metadata;

namespace PFA_ProjectAPI.Models.Domain
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid UserId { get; set; } 
        public User User { get; set; } 
    }
}
