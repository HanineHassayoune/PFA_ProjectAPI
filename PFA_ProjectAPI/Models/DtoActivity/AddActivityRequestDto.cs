﻿using System;
using System.ComponentModel.DataAnnotations;
using PFA_ProjectAPI.Models.Enums;

namespace PFA_ProjectAPI.Models.DTO
{
    public class AddActivityRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Animator { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}