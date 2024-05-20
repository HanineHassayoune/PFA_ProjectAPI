using PFA_ProjectAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DTO
{
    public class UpdateActivityRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public string Animator { get; set; }
    
        public string Pictures { get; set; }
        [Required]
        public string TeamBuilding { get; set; }
        [Required]
        public ActivityEnum Status { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
