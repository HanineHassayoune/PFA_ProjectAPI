using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DtoEvent
{
    public class AddEventRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

     
        public string Participants { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
