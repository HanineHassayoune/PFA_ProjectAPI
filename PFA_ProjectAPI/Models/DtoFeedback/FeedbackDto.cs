namespace PFA_ProjectAPI.Models.DtoFeedback
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }
    }
}
