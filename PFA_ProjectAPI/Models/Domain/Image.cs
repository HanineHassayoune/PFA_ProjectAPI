using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PFA_ProjectAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped] // To not store in database
        public IFormFile File { get; set; } // The type of file 

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }

        // Navigation property and foreign key for one-to-one relationship
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;


      

    }
}