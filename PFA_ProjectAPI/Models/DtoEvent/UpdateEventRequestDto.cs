using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DtoEvent
{
    public class UpdateEventRequestDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

        [Required]
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
