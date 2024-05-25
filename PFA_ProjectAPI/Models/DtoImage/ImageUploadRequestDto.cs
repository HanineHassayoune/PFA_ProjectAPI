﻿using System.ComponentModel.DataAnnotations;

namespace PFA_ProjectAPI.Models.DtoImage
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName{ get; set; }
    }
}
