using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DtoEvent
{
    public class UpdateEventRequestDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

       

        [Required]
        public string Creator { get; set; }

        [Required]
        public int Capacity { get; set; }


        [Required]
        public string Status { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
