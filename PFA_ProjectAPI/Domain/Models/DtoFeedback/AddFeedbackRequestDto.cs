using PFA_ProjectAPI.Migrations;


namespace PFA_ProjectAPI.Domain.Models.DtoFeedback
{
    public class AddFeedbackRequestDto
    {
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }
        public Guid EventId { get; set; }

    }
}
