using PFA_ProjectAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DTO
{
    public class AddActivityRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public string Animator { get; set; }
        [Required]
        public string Pictures { get; set; }
        public string TeamBuilding { get; set; }
        [Required]
        public ActivityEnum Status { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
