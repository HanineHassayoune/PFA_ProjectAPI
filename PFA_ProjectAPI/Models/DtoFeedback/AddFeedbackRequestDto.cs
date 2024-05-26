using PFA_ProjectAPI.Migrations;
using PFA_ProjectAPI.Models.Domain;

namespace PFA_ProjectAPI.Models.DtoFeadback
{
    public class AddFeedbackRequestDto
    {
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }
         public Guid EventId { get; set; }

    }
}
