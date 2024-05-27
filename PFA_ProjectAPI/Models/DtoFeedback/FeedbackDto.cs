namespace PFA_ProjectAPI.Models.DtoFeadback
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }
        public Guid EventId { get; set; }
    }
}
