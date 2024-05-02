﻿using PFA_ProjectAPI.Models.Enums;

namespace PFA_ProjectAPI.Models.DTO
{
    public class AddActivityRequestDto
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Animator { get; set; }
        public string Pictures { get; set; }
        public string TeamBuilding { get; set; }
        public ActivityEnum Status { get; set; }
        public string Description { get; set; }
    }
}