﻿namespace PFA_ProjectAPI.Models.DtoFeedback
{
    public class UpdateFeedbackRequestDto
    {
     
        public string Commentaire { get; set; }
        public string Emoji { get; set; }
        public int Stars { get; set; }
    }
}
